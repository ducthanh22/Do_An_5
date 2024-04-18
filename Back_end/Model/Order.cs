
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Order: Basedb
    {
        
        public Guid Id_customer {  get; set; }
        public int status { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }
        public string Payment  { get; set; }



}
}
