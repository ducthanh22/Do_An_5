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
    }
}
