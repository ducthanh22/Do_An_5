using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PaymentTransactions : Basedb
    {
        public Guid OrderId { get; set; }
        public int Money {  get; set; }
        public int TransactionStatus { get; set; }


    }
}
