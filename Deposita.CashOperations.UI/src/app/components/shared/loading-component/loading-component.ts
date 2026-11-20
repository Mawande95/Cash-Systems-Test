import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatProgressSpinnerModule }
from '@angular/material/progress-spinner';

import { LoadingService } from '../../../services/loading-service';

@Component({
  imports: [CommonModule,
    MatProgressSpinnerModule],
  standalone: true,
  selector: 'app-loading-component',
  styleUrl: './loading-component.css',
  templateUrl: './loading-component.html',
})
export class LoadingComponent {
    protected readonly loadingService =
    inject(LoadingService);
}
