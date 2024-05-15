using AutoMapper;
using DAL.Interface;
using DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ExportbillRepository : GenericRepository<Exportbill>, IExportbillRepository
    {
        public ExportbillRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<BaseQuerieResponse<ExportbillDto>> Search(string keyword, int page, int pageSize)
        {

            var query = from d in _DbContext.Set<Exportbill>().AsQueryable()
                        join a in _DbContext.User on d.IdStaff equals a.Id

                        where (string.IsNullOrEmpty(keyword)|| d.Id.ToString()==keyword || d.IdStaff.ToString()==keyword)
                        select new ExportbillDto
                        {
                            Id = d.Id,
                            Price=d.Price,
                            Status=d.Status,
                            IdStaff=d.IdStaff,
                            userName=a.UserName
                        };

            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<ExportbillDto>
            {
                PageIndex = page,
                PageSize = pageSize,
                Keyword = keyword,
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
                Created = DateTime.Now
            };

            var ExportbillEntity = _mapper.Map<Exportbill>(ExportDto);
            await _DbContext.Exportbill.AddAsync(ExportbillEntity);
            await _DbContext.SaveChangesAsync();

            foreach (var item in entity.Detail_exportbillDto)
            {
                Detail_exportbillDto DetailDto = new Detail_exportbillDto
                {
                    IdExportbill = ExportbillEntity.Id,
                    Idproduct = item.Idproduct,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Idsize=item.Idsize,
                    Created = DateTime.Now

                };

                var DetailEntity = _mapper.Map<Detail_exportbill>(DetailDto);
                await _DbContext.Detail_exportbill.AddAsync(DetailEntity);
                await _DbContext.SaveChangesAsync();
            }


            return entity;
        }
    }
}
