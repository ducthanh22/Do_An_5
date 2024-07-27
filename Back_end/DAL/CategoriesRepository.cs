
using AutoMapper;
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

        public async Task<BaseQuerieResponse<CategoriesDto>> Search(Paging paging)
        {

            var query = from d in _DbContext.Set<Categories>().AsQueryable()
                        where string.IsNullOrEmpty(paging.Keyword) || d.Name.Contains(paging.Keyword)
                        select new CategoriesDto 
                        {
                            Id = d.Id,
                        Name = d.Name,
                        };
  
            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize) .ToListAsync();
      
            var searchResults = new BaseQuerieResponse<CategoriesDto>
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                Keyword = paging.Keyword,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;
        }
    }
}