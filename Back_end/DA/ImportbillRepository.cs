﻿using AutoMapper;
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
    public class ImportbillRepository : GenericRepository<Importbill>, IImportbillRepository
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

        public async Task<CreateImportbillDto> CreateIm(CreateImportbillDto entity)
        {
            // Create and save the main order
            CreateImportbillDto orderDto = new CreateImportbillDto
            {
                IdStaff = entity.IdStaff,
                Status = entity.Status,
                Price = entity.Price,
            };

            var ImportbillEntity = _mapper.Map<Order>(orderDto);
            await _DbContext.Order.AddAsync(ImportbillEntity);
            await _DbContext.SaveChangesAsync();

            // Map and save order details
            foreach (var item in entity.Detail_importbill)
            {
                Detail_importbillDto DetailDto = new Detail_importbillDto
                {
                    IdImportbill = ImportbillEntity.Id,
                    Idproduct = item.Idproduct,
                    Quantity = item.Quantity,
                    Price = item.Price,
                };

                var DetailEntity = _mapper.Map<Detail_importbill>(DetailDto);
                await _DbContext.Detail_importbill.AddAsync(DetailEntity);
                await _DbContext.SaveChangesAsync();
            }


            return entity;
        }
    }
}
