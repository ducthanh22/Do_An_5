
using AutoMapper;
using Back_end.Model;
using DAL.Interface;
using DTO;
using Microsoft.EntityFrameworkCore;
using Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DAL
{
    public class CategoriesRepository : GenericRepository<Categories>, ICategoriesRepository
    {
        public CategoriesRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

        public async Task<BaseQuerieResponse<CategoriesDto>> Search(string keyword, int page, int pageSize)
        {

            var query = from d in _DbContext.Set<Categories>().AsQueryable()
                        where d.Name.Contains(keyword)
                        select new CategoriesDto 
                        {
                            Id = d.Id,
                        Name = d.Name,
                        };
  
            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((page - 1) * pageSize).Take(pageSize) .ToListAsync();
      
            var searchResults = new BaseQuerieResponse<CategoriesDto>
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