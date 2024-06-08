using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Sale : Basedb
    {
        public Guid IdProduct { get; set; }
        public int? SalePrice { get; set; }
        public int? percent {  get; set; }
        public int? SaleTime { get; set; }
        public int? Time_remaining { get; set; }

    }

}
