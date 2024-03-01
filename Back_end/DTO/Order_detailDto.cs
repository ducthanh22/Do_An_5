using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Order_detailDto : BasedbDto
    {
        public Guid Id_Order { get; set; }
        public int Id_product { get; set; }
        public int Quantity { get; set;}
        public int Price { get; set; }

    }
}
