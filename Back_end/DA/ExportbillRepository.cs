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
    public class ExportbillRepository : GenericRepository<Exportbill>, IExportbillRepository
    {
        public ExportbillRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<BaseQuerieResponse<ExportbillDto>> Search(int keyword, int page, int pageSize)
        {

            var query = from d in _DbContext.Set<Exportbill>().AsQueryable()

                        where d.Price == keyword
                        select new ExportbillDto
                        {
                            Price=d.Price,
                            

                           
                        };

            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<ExportbillDto>
            {
                PageIndex = page,
                PageSize = pageSize,
                Keynumber = keyword,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;
        }
        public async Task<CreateExportbillDto> CreateEX(CreateExportbillDto entity)
        {
            // Create and save the main order
            CreateExportbillDto ExportDto = new CreateExportbillDto
            {
                IdStaff = entity.IdStaff,
                Status = entity.Status,
                Price = entity.Price,
            };

            var ExportbillEntity = _mapper.Map<Order>(ExportDto);
            await _DbContext.Order.AddAsync(ExportbillEntity);
            await _DbContext.SaveChangesAsync();

            // Map and save order details
            foreach (var item in entity.Detail_exportbillDto)
            {
                Detail_exportbillDto DetailDto = new Detail_exportbillDto
                {
                    IdExportbill = ExportbillEntity.Id,
                    Idproduct = item.Idproduct,
                    Quantity = item.Quantity,
                    Price = item.Price,
                };

                var DetailEntity = _mapper.Map<Detail_exportbill>(DetailDto);
                await _DbContext.Detail_exportbill.AddAsync(DetailEntity);
                await _DbContext.SaveChangesAsync();
            }


            return entity;
        }
    }
}
