import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { TransactionService } from '../../../services/transaction.service';
import { TransactionRequest } from '../../../models/transaction.request.model';
import { NotificationService } from '../../../services/notification-service';

@Component({
  imports: [
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
  ],
  selector: 'app-transaction-dialog.component.ts',
  styleUrl: './transaction-dialog.component.ts.css',
  templateUrl: './transaction-dialog.component.ts.html',
})
export class TransactionDialogComponent {
  private readonly fb = inject(FormBuilder);
  private readonly transactionService = inject(TransactionService);
  private readonly dialogRef = inject(MatDialogRef<TransactionDialogComponent>);
  isSaving = false;
  readonly data = inject(MAT_DIALOG_DATA, { optional: true });
  readonly isEditMode = !!this.data;
  private readonly notificationService = inject(NotificationService);

  constructor() {
    if (this.data) {
      this.form.patchValue({
        amount: this.data.amount,
        description: this.data.description,
        transactionType: this.data.transactionType,
      });
    }
  }

  readonly transactionTypes = ['Deposit', 'Withdrawal', 'Transfer'];

  readonly form = this.fb.group({
    amount: [null, [Validators.required, Validators.min(0.01)]],
    description: ['', [Validators.required, Validators.maxLength(255)]],
    transactionType: ['', Validators.required],
  });

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const payload: TransactionRequest = {
      amount: this.form.value.amount!,
      description: this.form.value.description!,
      transactionType: this.form.value.transactionType!,
    };

    if (this.isEditMode) {
      this.transactionService.update(this.data.transactionId.toString(), payload).subscribe({
        next: (response) => {
          this.dialogRef.close(response);
        },
        error: (error) => {
          this.notificationService.error(error.error.Message);
        },
      });

      return;
    }

    this.transactionService.create(payload).subscribe({
      next: (response) => {
        this.dialogRef.close(response);
      },
      error: (error) => {
        this.notificationService.error(error.error.Message);
      },
    });
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
