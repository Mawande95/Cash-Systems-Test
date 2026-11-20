export interface Transaction {
  transactionId: number;
  dateCreated: string;
  amount: number;
  description: string;
  transactionType: string;
}