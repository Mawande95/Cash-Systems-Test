import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';

@Component({
  imports: [MatToolbarModule, MatIconModule],
  selector: 'app-header-component',
  styleUrl: './header-component.css',
  templateUrl: './header-component.html',
})
export class HeaderComponent {}
