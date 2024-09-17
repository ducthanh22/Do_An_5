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
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace DAL
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ISendEmailRepository _sendEmailRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public OrderRepository(Achino_DbContext dbContext, IMapper mapper, ISendEmailRepository sendEmailRepository,IWebHostEnvironment hostingEnvironment) : base(dbContext, mapper)
        {
            _sendEmailRepository = sendEmailRepository;
            _hostingEnvironment = hostingEnvironment;

        }
        public async Task<List<OrderDto>> GetbyCustomer(string id)
        {
            var query = from a in _DbContext.Order
                        where (a.Id_customer == id)
                        orderby a.Created descending

                        select new OrderDto
                        {
                            Id = a.Id,
                            Id_customer = a.Id_customer,
                            Price = a.Price,
                            Payment = a.Payment,
                            status = a.status,
                            Address = a.Address
                        };

            return await query.ToListAsync();
        }

        public async Task<List<GetorderDto>> GetOrderProduct(string id, int status)
        {

            var query = from a in _DbContext.Order
                        join b in _DbContext.Order_detail on a.Id equals b.Id_Order
                        join c in _DbContext.Products on b.Id_product equals c.Id
                        join d in _DbContext.User on a.Id_customer equals d.Id
                        where (a.Id_customer == id && a.status == status)
                        group new { c, b } by new { a.Id, a.Id_customer, a.Price, a.Address, a.Payment, a.status, a.Created, d.UserName, d.Email, d.PhoneNumber } into g
                        orderby g.Key.Created descending
                        select new GetorderDto
                        {
                            Id = g.Key.Id,
                            Id_customer = g.Key.Id_customer,
                            Price = g.Key.Price,
                            Address = g.Key.Address,
                            Payment = g.Key.Payment,
                            Status = g.Key.status,
                            Quantity = g.Count(),
                            Username = g.Key.UserName,
                            Phone = g.Key.PhoneNumber,
                            Email = g.Key.Email,
                            Created = g.Key.Created,
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
        public async Task<List<GetorderDto>> Getbyids(Guid id)
        {

            var query = from a in _DbContext.Order
                        join b in _DbContext.Order_detail on a.Id equals b.Id_Order
                        join c in _DbContext.Products on b.Id_product equals c.Id
                        join d in _DbContext.User on a.Id_customer equals d.Id
                        join e in _DbContext.Size on c.Id equals e.Idproduct
                        where (a.Id == id && b.Idsize == e.Id)
                        group new { c, b, e } by new { a.Id, a.Id_customer, a.Price, a.Address, a.Payment, a.status, a.Created, d.UserName, d.Email, d.PhoneNumber } into g
                        orderby g.Key.Created descending
                        select new GetorderDto
                        {
                            Id = g.Key.Id,
                            Id_customer = g.Key.Id_customer,
                            Price = g.Key.Price,
                            Address = g.Key.Address,
                            Payment = g.Key.Payment,
                            Status = g.Key.status,
                            Quantity = g.Count(),
                            Username = g.Key.UserName,
                            Phone = g.Key.PhoneNumber,
                            Email = g.Key.Email,
                            Created = g.Key.Created,
                            OrderProductList = g.Select(x => new Order_productDto
                            {
                                Id_product = x.c.Id,
                                Image = x.c.Image,
                                Product_name = x.c.Name,
                                Quantity = x.b.Quantity,
                                Price = x.b.Price,
                                Id_size = x.e.Id,
                            }).ToList()
                        };

            return await query.ToListAsync();
        }
        public async Task<BaseQuerieResponse<OrderDto>> Search(Paging paging, int status)
        {
            var query = from d in _DbContext.Set<Order>()
                        join a in _DbContext.User on d.Id_customer equals a.Id
                        where ((string.IsNullOrEmpty(paging.Keyword)&& d.status==status) || (a.UserName.Contains(paging.Keyword) && d.status == status) ||( a.Email.Contains(paging.Keyword) && d.status == status))
                        orderby d.Created descending

                        select new OrderDto
                        {
                            Id = d.Id,
                            Id_customer = d.Id_customer,
                            Price = d.Price,
                            Payment = d.Payment,
                            status = d.status,
                            Address = d.Address,
                            Username = a.UserName,
                            Created = d.Created,

                        };

            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<OrderDto>
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                Keyword = paging.Keyword,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;
        }
        public async Task<CreateOrderDto> CreateOrder(CreateOrderDto entity)
        {
            // Tạo chiến lược thực thi
            var strategy = _DbContext.Database.CreateExecutionStrategy();

            // Thực thi mã trong chiến lược thực thi
            return await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _DbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var checkUser = await _DbContext.User.FindAsync(entity.Id_customer);

                        CreateOrderDto orderDto = new CreateOrderDto
                        {
                            Id_customer = entity.Id_customer,
                            status = entity.status,
                            Price = entity.Price,
                            Address = entity.Address,
                            Payment = entity.Payment,
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
                                Idsize = item.Idsize,
                                Quantity = item.Quantity,
                                Price = item.Price,
                                Created = DateTime.Now,
                            };
                            var orderDetailEntity = _mapper.Map<Order_detail>(orderDetailDto);
                            await _DbContext.Order_detail.AddAsync(orderDetailEntity);

                            var checkwarehouse = await _DbContext.Detail_warehouse
                                .Where(x => x.Idproduct == item.Id_product && x.Idsize == item.Idsize)
                                .FirstOrDefaultAsync();

                            if (checkwarehouse != null)
                            {
                                checkwarehouse.Quantity -= item.Quantity;
                                _DbContext.Detail_warehouse.Update(checkwarehouse);
                            }
                            else
                            {
                                Detail_warehouse detail_Warehouse = new Detail_warehouse
                                {
                                    Idwarehouse = Guid.NewGuid(),
                                    Idproduct = orderDetailEntity.Id_product,
                                    Quantity = orderDetailEntity.Quantity,
                                    Idsize = orderDetailEntity.Idsize,
                                };
                                await _DbContext.Detail_warehouse.AddAsync(detail_Warehouse);
                            }
                            await _DbContext.SaveChangesAsync();
                        }

                        await transaction.CommitAsync();

                        // Gửi email xác nhận đơn hàng
                        if (entity.status == 1)
                        {
                            var callbackUrl = "http://localhost:4200/client/confirmOder/" + entity.Id;
                            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Temlate_Email", "confirmOrder.html");
                            string htmlMessage = await System.IO.File.ReadAllTextAsync(htmlFilePath);
                            htmlMessage = htmlMessage.Replace("{{callbackUrl}}", callbackUrl);
                            htmlMessage = htmlMessage.Replace("{{Username}}", checkUser.UserName);

                            await _sendEmailRepository.SendEmailAsync(checkUser.Email, "Xác nhận đơn hàng", htmlMessage);
                        }

                        return entity;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        // Hoàn tác giao dịch nếu có lỗi xảy ra
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            });
        }

        public async Task<Order>destroyOrder(Guid id)
        {
            var checkdetail = await _DbContext.Order_detail.Where(x=>x.Id_Order== id).ToListAsync();
            foreach( var item in checkdetail )
            {
                var checkwarehouse = await _DbContext.Detail_warehouse.Where(x => x.Idproduct == item.Id_product).FirstOrDefaultAsync();
                if ( checkwarehouse != null )
                {
                    checkwarehouse.Quantity += item.Quantity;
                    _DbContext.Detail_warehouse.Update(checkwarehouse);
                    await _DbContext.SaveChangesAsync();
                }
            }
            var checkorder = await _DbContext.Order.Where(x=>x.Id==id).FirstOrDefaultAsync();
            if ( checkorder != null )
            {
                checkorder.status = 7;
                _DbContext.Order.Update(checkorder);
                await _DbContext.SaveChangesAsync();
            }
            return checkorder;
        }
    }
}
