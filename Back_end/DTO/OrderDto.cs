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
        //public Guid Id { get; set; }
        public Guid Id_customer { get; set; }
        public int status { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }
        public string Payment { get; set; }
        public List<Order_detailDto> OrderList { get; set; }

    }
    public class GetorderDto : BasedbDto
    {
        public Guid Id_customer { get; set; }
        public int Status { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }
        public string Payment { get; set; }
        public int Quantity { get; set; }

        public List<Order_productDto> OrderProductList { get; set; }

    }
    public class Order_productDto
    {
        public Guid Id_product { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public int? Price { get; set; }

    }

}
