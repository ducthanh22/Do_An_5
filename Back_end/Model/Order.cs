
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Order: Basedb
    {
        
        public int Id_customer {  get; set; }
        public bool status { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
