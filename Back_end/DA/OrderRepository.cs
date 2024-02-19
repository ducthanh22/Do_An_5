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
    public class OrderRepository : GenericRepository<OrderDto>, IOrderRepository
    {
        public OrderRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<CreateOrderDto> CreateOrder(CreateOrderDto entity)
        {
            // Create and save the main order
            CreateOrderDto orderDto = new CreateOrderDto
            {
                Id_customer = entity.Id_customer,
                status = entity.status,
                OrderDate = entity.OrderDate,
            };

            var orderEntity = _mapper.Map<Order>(orderDto);
            await _DbContext.Order.AddAsync(orderEntity);
            await _DbContext.SaveChangesAsync();

            // Map and save order details
            foreach (var item in entity.OrderList)
            {
                Order_detailDto orderDetailDto = new Order_detailDto
                {
                    Id_Order = orderEntity.Id, 
                    Id_product = item.Id_product,
                    Quantity = item.Quantity,
                    Price = item.Price,
                };

                var orderDetailEntity = _mapper.Map<Order_detail>(orderDetailDto);
                await _DbContext.Order_detail.AddAsync(orderDetailEntity);
                await _DbContext.SaveChangesAsync();
            }
            

            return entity;
        }

    }

}
