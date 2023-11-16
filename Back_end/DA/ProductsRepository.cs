
using AutoMapper;
using Back_end.Model;
using DAL.Interface;
using DTO;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL
{
    public class ProductsRepository : GenericRepository<Products>, IProductsRepository
    {
        public ProductsRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<BaseQuerieResponse<ProductsDto>> Search(string keyword, int page, int pageSize)
        {

            var query = from d in _DbContext.Set<Products>().AsQueryable()
                        where d.Name.Contains(keyword)
                        select new ProductsDto
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Idcategories = d.Idcategories,
                            Idproduces = d.Idproduces,
                            Image=d.Image,
                            Describe=d.Describe
                           
                        };

            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<ProductsDto>
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