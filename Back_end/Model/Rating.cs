using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Rating :Basedb
    {
        public Guid Id_Order { get; set; }
        public Guid Id_product { get; set; }
        public string Id_customer { get; set; }
        public int Evaluate {  get; set; }
        public string Comment { get; set; }
        public int Status { get; set; }



    }
}
