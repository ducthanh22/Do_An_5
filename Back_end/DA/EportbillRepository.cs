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
    public class EportbillRepository : GenericRepository<ExportbillDto>, IExportbillRepository
    {
        public EportbillRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<BaseQuerieResponse<ExportbillDto>> Search(int keyword, int page, int pageSize)
        {

            var query = from d in _DbContext.Set<Exportbill>().AsQueryable()

                        where d.Price == keyword
                        select new ExportbillDto
                        {
                            Price=d.Price,
                            Startday=d.Startday,
                            Endday=d.Endday,

                           
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
    }
}
