using BUS.Interface;
using DAL.Interface;
using DTO;
using Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class Order_detailBus : GenericBus<Order_detail>,IOder_detailBus
    {
        public IOrder_detailRepository _res;
        public Order_detailBus(IOrder_detailRepository res) : base(res)
        {
            _res = res;
        }
    }
}
