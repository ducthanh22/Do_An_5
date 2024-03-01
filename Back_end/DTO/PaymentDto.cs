using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PaymentDto:BasedbDto
    {
        public Guid OrderId { get; set; }
        public int Money { get; set; }
        public int TransactionStatus { get; set; }
    }
    public class VnPay 
    {
        public string vnp_Url { get; set; }
        public string vnp_TmnCode { get; set; }
        public string vnp_HashSecret { get; set; }
        public string vnp_Version { get; set; }
        public string vnp_CurrCode { get; set; }
        public string vnp_Locale { get; set; }
        public string vnp_Command { get; set; }
        public string vnp_ReturnUrl { get; set; }
    }
    public class PaymentRespone
    {
        public string vnp_Amount { get; set; }
        public string vnp_BankCode { get; set; }
        public string vnp_OrderInfo { get; set; }
        public string vnp_TransactionStatus { get; set; }
        public string vnp_TxnRef { get; set; }
    }

}
