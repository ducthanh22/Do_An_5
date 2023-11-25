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
    public class ProducesRepository : GenericRepository<Produces>, IProducesRepository
    {
        public ProducesRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<BaseQuerieResponse<ProducesDto>> Search(string keyword, int page, int pageSize)
        {

            var query = from d in _DbContext.Set<Produces>().AsQueryable()
                        where string.IsNullOrEmpty(keyword) || d.Name.Contains(keyword)
                        select new ProducesDto
                        {
                            Id = d.Id,
                            Name = d.Name,
                           Email = d.Email,
                            Address = d.Address,
                        };

            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<ProducesDto>
            {
                PageIndex = page,
                PageSize = pageSize,
                Keyword = keyword,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;
        }
    }
}
