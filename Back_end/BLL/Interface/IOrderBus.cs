using Model;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IOrderBus :IGenericBUS<Order>
    {
        Task<CreateOrderDto> CreateOrder(CreateOrderDto entity);

    }
}
