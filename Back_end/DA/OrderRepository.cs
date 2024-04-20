using AutoMapper;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using Model;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<List<OrderDto>> GetbyCustomer(Guid id)
        {
            var query = from a in _DbContext.Order
                        where (a.Id_customer ==id)
                        select new OrderDto
                        {
                            Id =a.Id,
                            Id_customer = a.Id_customer,
                            Price = a.Price,
                            Payment=a.Payment,
                            status=a.status,
                            Address=a.Address
                        };

            return await query.ToListAsync();
        }

        public async Task<CreateOrderDto> CreateOrder(CreateOrderDto entity)
        {
            // Create and save the main order
            CreateOrderDto orderDto = new CreateOrderDto
            {
                Id_customer = entity.Id_customer,
                status = entity.status,
                Price= entity.Price,
                Address=entity.Address,
                Payment=entity.Payment,
                Created = DateTime.Now,

            };
            var orderEntity = _mapper.Map<Order>(orderDto);
            await _DbContext.Order.AddAsync(orderEntity);
            await _DbContext.SaveChangesAsync();
            entity.Id = orderEntity.Id;
            // Map and save order details
            foreach (var item in entity.OrderList)
            {
                Order_detailDto orderDetailDto = new Order_detailDto
                {
                    Id_Order = orderEntity.Id, 
                    Id_product = item.Id_product,
                    Idsize=item.Idsize,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Created=DateTime.Now,
                };
                var orderDetailEntity = _mapper.Map<Order_detail>(orderDetailDto);
                await _DbContext.Order_detail.AddAsync(orderDetailEntity);
                await _DbContext.SaveChangesAsync();
            }     
            return entity;
        }

    }

}
