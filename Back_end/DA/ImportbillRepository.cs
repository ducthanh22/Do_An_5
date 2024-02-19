using AutoMapper;
using DAL.Interface;
using DTO;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ImportbillRepository : GenericRepository<ImportbillDto>, IImportbillRepository
    {
        public ImportbillRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<BaseQuerieResponse<ImportbillDto>> Search(int keyword, int page, int pageSize)
        {
            var query = from d in _DbContext.Set<Importbill>().AsQueryable()
                        where d.Price==keyword

                        select new ImportbillDto
                        {
                            Price = d.Price,
                            Startday = d.Startday,
                            Endday = d.Endday,
                            
                        };

            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<ImportbillDto>
            {
                PageIndex = page,
                PageSize = pageSize,
                Keynumber = keyword,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;
        }

        public async Task<CreateOrderDto> CreateIm(CreateOrderDto entity)
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
