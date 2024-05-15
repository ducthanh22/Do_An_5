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

        public string Id_customer { get; set; }
        public int status { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }
        public string Payment  { get; set; }
        public string Username { get; set; }




    }
    public class CreateOrderDto : BasedbDto
    {
        //public Guid Id { get; set; }
        public string Id_customer { get; set; }
        public int status { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }
        public string Payment { get; set; }
        public List<Order_detailDto> OrderList { get; set; }

    }
    public class GetorderDto : BasedbDto
    {
        public string Id_customer { get; set; }
        public int Status { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }
        public string Payment { get; set; }
        public int Quantity { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }


        public List<Order_productDto> OrderProductList { get; set; }

    }
    public class Order_productDto
    {
        public Guid Id_product { get; set; }
        public string Image { get; set; }
        public string Product_name { get; set; }
        public Guid Id_size { get; set; }

        public int Quantity { get; set; }
        public int? Price { get; set; }

    }

}
