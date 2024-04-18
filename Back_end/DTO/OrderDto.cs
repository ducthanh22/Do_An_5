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

        public Guid Id_customer { get; set; }
        public int status { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }
        public string Payment  { get; set; }



    }
    public class CreateOrderDto : BasedbDto
    {
        public Guid Id { get; set; }

        public Guid Id_customer { get; set; }
        public int status { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }
    public string Payment  { get; set; }

public List<Order_detailDto> OrderList { get; set; }

    }
}
