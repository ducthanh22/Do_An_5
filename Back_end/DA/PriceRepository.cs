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
    public class PriceRepository : GenericRepository<Price>, IPriceRepository
    {
        public PriceRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<BaseQuerieResponse<PriceDto>> Search(int? keyword, int page, int pageSize)
        {

            var query = from d in _DbContext.Set<Price>().AsQueryable()
                        where d.Price_product==keyword
                        select new PriceDto
                        {
                            Price_product=d.Price_product,
                            Startday=d.Startday,
                            Endday=d.Endday,
                        };

            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<PriceDto>
            {
                PageIndex = page,
                PageSize = pageSize,
                Keynumber = keyword,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;
        }
    }
}
