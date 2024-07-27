using AutoMapper;
using DAL.Interface;
using DTO;
using Microsoft.EntityFrameworkCore;
using Model;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ImportbillRepository : GenericRepository<Importbill>, IImportbillRepository
    {
        public ImportbillRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<BaseQuerieResponse<ImportbillDto>> Search(Paging paging)
        {
            var query = from d in _DbContext.Set<Importbill>().AsQueryable()
                        join a in _DbContext.User on d.IdStaff equals a.Id
                        where (string.IsNullOrEmpty(paging.Keyword) || a.PhoneNumber.Contains(paging.Keyword) || a.Email.Contains(paging.Keyword)||a.UserName.Contains(paging.Keyword))
                        orderby d.Created descending
                        select new ImportbillDto
                        {
                            Id = d.Id,
                            Price = d.Price,
                            Status = d.Status,
                            IdStaff = d.IdStaff,
                            userName = a.UserName
                        };


            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<ImportbillDto>
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                Keyword = paging.Keyword,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;
        }

        public async Task<CreateImportbillDto> CreateIm(CreateImportbillDto entity)
        {
            using (var transaction = await _DbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    CreateImportbillDto importbillDto = new CreateImportbillDto
                    {
                        IdStaff = entity.IdStaff,
                        Status = entity.Status,
                        Price = entity.Price,
                        Created = DateTime.Now

                    };
                    var ImportbillEntity = _mapper.Map<Importbill>(importbillDto);
                    await _DbContext.Importbill.AddAsync(ImportbillEntity);
                    await _DbContext.SaveChangesAsync();
                    entity.Id= ImportbillEntity.Id;
                    foreach (var item in entity.Detail_importbill)
                    {
                        Detail_importbillDto DetailDto = new Detail_importbillDto
                        {
                            IdImportbillId = ImportbillEntity.Id,
                            Idproduct = item.Idproduct,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            Idsize=item.Idsize,
                            Created = DateTime.Now,

                        };

                        var DetailEntity = _mapper.Map<Detail_importbill>(DetailDto);
                        await _DbContext.Detail_importbill.AddAsync(DetailEntity);
                        await _DbContext.SaveChangesAsync();
                        item.IdImportbillId = DetailEntity.IdImportbillId;
                        item.Id=DetailEntity.Id;

                        // Checking if the product exists in the Detail_warehouse table based on Idproduct
                        var checkwarehouse = await _DbContext.Detail_warehouse.Where(x=>x.Idproduct==item.Idproduct && x.Idsize==item.Idsize).FirstOrDefaultAsync();

                        if (checkwarehouse != null)
                        {
                            checkwarehouse.Quantity += item.Quantity;

                            _DbContext.Detail_warehouse.Update(checkwarehouse);
                            await _DbContext.SaveChangesAsync();
                        }
                        else
                        {
                            Detail_warehouse detail_Warehouse = new Detail_warehouse
                            {
                                Idwarehouse = Guid.NewGuid(),
                                Idproduct = DetailEntity.Idproduct,
                                Quantity = DetailEntity.Quantity,
                                Idsize = DetailEntity.Idsize,
                            };

                            //var data = _mapper.Map<Detail_warehouse>(DetailDto);
                            await _DbContext.Detail_warehouse.AddAsync(detail_Warehouse);
                            await _DbContext.SaveChangesAsync();
                        }

                    }


                    // Commit transaction if all operations are successful
                    await transaction.CommitAsync();

                    return entity;
                }
                catch (Exception)
                {
                    // Rollback transaction if any operation fails
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

    }
}
