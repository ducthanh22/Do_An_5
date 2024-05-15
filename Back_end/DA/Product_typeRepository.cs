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
    public class Product_typeRepository:GenericRepository<Product_type>,IProduct_typeRepository
    {
        public Product_typeRepository(Achino_DbContext dbContext, IMapper mapper):base(dbContext,mapper) {
        }
        public async Task<BaseQuerieResponse<Product_typeDto>> Search(Paging paging)
        {

            var query = from d in _DbContext.Product_type.AsQueryable()
                        where string.IsNullOrEmpty(paging.Keyword) || d.Name.Contains(paging.Keyword)
                        select new Product_typeDto
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Idcategories=d.Idcategories
                        };

            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<Product_typeDto>
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                Keyword = paging.Keyword,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;
        }
        public async Task<List<Product_typeDto>> GetByCategory( Guid id)
        {
             var query = from a in _DbContext.Product_type
                         join b in _DbContext.Categorie on a.Idcategories equals b.Id
                         where a.Idcategories==id
                         select new Product_typeDto
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Idcategories = b.Id
                         };

            return await query.ToListAsync();

        }
    }
}
