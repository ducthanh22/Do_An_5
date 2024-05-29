using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<CreateOrderDto> CreateOrder(CreateOrderDto entity);
        Task<List<OrderDto>> GetbyCustomer(string id);
        Task<List<GetorderDto>> GetOrderProduct(string id, int status);
        Task<BaseQuerieResponse<OrderDto>> Search(Paging paging);
        Task<List<GetorderDto>> Getbyids(Guid id);
        Task<Order> destroyOrder(Guid id);



    }
}
