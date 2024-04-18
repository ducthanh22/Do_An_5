using BLL.Interface;
using DAL.Interface;
using Model;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderBus : GenericBus<Order>, IOrderBus
    {
        public IOrderRepository _res;
        public OrderBus(IOrderRepository res) : base(res)
        {
            _res = res;
        }

        
        public async Task<List<OrderDto>> GetbyCustomer(Guid id)
        {
            return await _res.GetbyCustomer(id);

        }
        public async Task<CreateOrderDto> CreateOrder(CreateOrderDto entity)
        {
            return await _res.CreateOrder(entity);
        }
    }
}
