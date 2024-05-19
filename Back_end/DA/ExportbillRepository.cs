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
            using (var transaction = await _DbContext.Database.BeginTransactionAsync())
            {
                try
                {
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

                    // Tạo và lưu các chi tiết đơn hàng (Detail_exportbill)
                    foreach (var item in entity.Detail_exportbillDto)
                    {
                        Detail_exportbillDto DetailDto = new Detail_exportbillDto
                        {
                            IdExportbill = ExportbillEntity.Id,
                            Idproduct = item.Idproduct,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            Idsize = item.Idsize,
                            Created = DateTime.Now
                        };
                        var DetailEntity = _mapper.Map<Detail_exportbill>(DetailDto);
                        await _DbContext.Detail_exportbill.AddAsync(DetailEntity);
                        await _DbContext.SaveChangesAsync();
                    }
                    await transaction.CommitAsync();
                    return entity;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Error creating records", ex);
                }
            }

        }
        public async Task<Exportbill> DELETE(Guid id)
        {
            try
            {
                var exportBill = await _DbContext.Exportbill.FindAsync(id);
                var detailExportBills = await _DbContext.Detail_exportbill
                    .Where(detail => detail.IdExportbill == id)
                    .ToListAsync();
                _DbContext.Detail_exportbill.RemoveRange(detailExportBills);
                _DbContext.Exportbill.Remove(exportBill);
                await _DbContext.SaveChangesAsync();
                return exportBill;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting records", ex);
            }

        }

    }
}
