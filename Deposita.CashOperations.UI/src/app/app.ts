import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './components/shared/header-component/header-component';
import { FooterComponent } from './components/shared/footer-component/footer-component';
import { TransactionComponent } from './components/transaction-component/transaction-component';
import { LoadingComponent } from './components/shared/loading-component/loading-component'

@Component({
  imports: [
            HeaderComponent,
            FooterComponent,
            TransactionComponent,
            LoadingComponent],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
  standalone: true,
})
export class App {
  protected readonly title = signal('Deposita.CashOperations.UI');
}
