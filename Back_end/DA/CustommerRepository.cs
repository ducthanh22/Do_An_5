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
    public class CustommerRepository : GenericRepository<Custommer>, ICustommerRepository
    {
        public CustommerRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<BaseQuerieResponse<CustommerDto>> Search(string keyword, int page, int pageSize)
        {

            var query = from d in _DbContext.Set<Custommer>().AsQueryable()
                        where string.IsNullOrEmpty(keyword) || d.Name.Contains(keyword)
                        select new CustommerDto
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Sdt = d.Sdt,
                            Address = d.Address,
                        };

            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<CustommerDto>
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
