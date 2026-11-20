import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Transaction } from '../models/transaction.model';
import { environment } from '../environments/environment';
import { TransactionRequest } from '../models/transaction.request.model';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  private readonly http = inject(HttpClient);
  private readonly endpoint = `${environment.apiUrl}/transactions`;

  getAll(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(this.endpoint);
  }

  create(payload: TransactionRequest): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(this.endpoint, payload);
  }

  update(id: string, payload: TransactionRequest): Observable<{ message: string }> {
    return this.http.put<{ message: string }>(`${this.endpoint}/${id}`, payload);
  }

  delete(id: string): Observable<{ message: string }> {
  return this.http.delete<{ message: string }>(
    `${this.endpoint}/${id}`
  );
}
}