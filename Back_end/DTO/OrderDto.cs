using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderDto: BasedbDto
    {

        public int Id_customer { get; set; }
        public int status { get; set; }
        public int Price { get; set; }


    }
    public class CreateOrderDto : BasedbDto
    {
        public int Id_customer { get; set; }
        public int status { get; set; }
        public int Price { get; set; }
        public List<Order_detailDto> OrderList { get; set; }

    }
}
