using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IOrderRepository : IGenericRepository<OrderDto>
    {
        Task<CreateOrderDto> CreateOrder(CreateOrderDto entity);
    }
}
