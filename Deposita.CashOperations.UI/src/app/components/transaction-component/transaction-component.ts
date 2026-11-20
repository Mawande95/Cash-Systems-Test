import { Component, OnInit, inject } from '@angular/core';
import { TransactionService } from '../../services/transaction.service';
import { Transaction } from '../../models/transaction.model';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { TransactionDialogComponent } from './transaction-dialog.component.ts/transaction-dialog.component.ts';
import { ConfirmationDialogComponent } from '../shared/confirmation-dialog.component/confirmation-dialog.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { NotificationService } from '../../services/notification-service';

@Component({
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatSnackBarModule,
  ],
  selector: 'app-transaction-component',
  styleUrl: './transaction-component.css',
  templateUrl: './transaction-component.html',
})
export class TransactionComponent implements OnInit {
  private readonly transactionService = inject(TransactionService);
  private readonly dialog = inject(MatDialog);
  private readonly notificationService = inject(NotificationService);

  displayedColumns: string[] = [
    'amount',
    'description',
    'transactionType',
    'dateCreated',
    'actions',
  ];

  dataSource = new MatTableDataSource<Transaction>();

  ngOnInit(): void {
    this.loadTransactions();
  }

  private loadTransactions(): void {
    this.transactionService.getAll().subscribe({
      next: (transactions) => {
        this.dataSource.data = transactions;
      },
      error: (error) => {
        console.error('Failed to load transactions.', error);
      },
    });
  }

  onAddTransaction(): void {
    const dialogRef = this.dialog.open(TransactionDialogComponent, {
      width: '600px',
      disableClose: true,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.notificationService.success(result.message);

        this.loadTransactions();
      }
    });
  }

  onEdit(transaction: Transaction): void {
    const dialogRef = this.dialog.open(TransactionDialogComponent, {
      width: '600px',
      disableClose: true,
      data: transaction,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.notificationService.success(result.message);
        this.loadTransactions();
      }
    });
  }

  onDelete(transaction: Transaction): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (!result) {
        return;
      }

      this.transactionService.delete(transaction.transactionId.toString()).subscribe({
        next: (response) => {
          this.notificationService.success(response.message);
          this.loadTransactions();
        },
        error: (error) => {
          this.notificationService.error(error.error.Message);
        },
      });
    });
  }
}
