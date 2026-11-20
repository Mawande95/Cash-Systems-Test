import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TransactionDialogComponentTs } from './transaction-dialog.component.ts';

describe('TransactionDialogComponentTs', () => {
  let component: TransactionDialogComponentTs;
  let fixture: ComponentFixture<TransactionDialogComponentTs>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TransactionDialogComponentTs],
    }).compileComponents();

    fixture = TestBed.createComponent(TransactionDialogComponentTs);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
