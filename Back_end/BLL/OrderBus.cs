using BUS.Interface;
using DAL.Interface;
using Model;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class OrderBus : GenericBus<Order>, IOrderBus
    {
        public IOrderRepository _res;
        public OrderBus(IOrderRepository res) : base(res)
        {
            _res = res;
        }


        public async Task<CreateOrderDto> CreateOrder(CreateOrderDto entity)
        {
            return await _res.CreateOrder(entity);
        }
    }
}
