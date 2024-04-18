

export interface PaymentDto  {
    orderId: string;
    money: number;
    transactionStatus: number;
}

export interface PaymentRespone {
    vnp_Amount: string;
    vnp_BankCode: string;
    vnp_OrderInfo: string;
    vnp_TransactionStatus: string;
    vnp_TxnRef: string;
}