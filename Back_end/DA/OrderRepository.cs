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
using static System.Net.Mime.MediaTypeNames;

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
                        orderby a.Created descending

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

        public async Task<List<GetorderDto>> GetOrderProduct(Guid id, int status)
        {

            var query = from a in _DbContext.Order
                        join b in _DbContext.Order_detail on a.Id equals b.Id_Order 
                        join c in _DbContext.Products on b.Id_product equals c.Id 
                        where (a.Id_customer == id && a.status == status)
                        group new { c, b } by new { a.Id, a.Id_customer, a.Price, a.Address, a.Payment, a.status ,a.Created} into g
                        orderby g.Key.Created descending
                        select new GetorderDto
                        {
                            Id = g.Key.Id,
                            Id_customer = g.Key.Id_customer,
                            Price = g.Key.Price,
                            Address = g.Key.Address,
                            Payment = g.Key.Payment,
                            Status = g.Key.status,
                            Quantity= g.Count(),
                            Created= g.Key.Created,
                            OrderProductList = g.Select(x => new Order_productDto
                            {
                                Id_product = x.c.Id,
                                Image = x.c.Image,
                                Product_name = x.c.Name,
                                Quantity = x.b.Quantity,
                                Price = x.b.Price
                            }).ToList()
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
