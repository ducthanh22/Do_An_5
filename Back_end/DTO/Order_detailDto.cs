using Back_end.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Order_detailDto : BasedbDto
    {
        public int Id_Order { get; set; }
        public int Id_product { get; set; }
        public int Quantity { get; set;}
        public int Price { get; set; }

    }
}
