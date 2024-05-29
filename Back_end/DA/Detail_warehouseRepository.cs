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
    public class Detail_warehouseRepository : GenericRepository<Detail_warehouse>, IDetail_warehouseRepository
    {
        public Detail_warehouseRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<countProduct> CountProduct(Guid id)
        {
            var query = from a in _DbContext.Detail_warehouse
                        where a.Idproduct == id
                        group a by a.Idproduct into g
                        select new countProduct
                        {
                            Idproduct = g.Key,
                            TotalQuantity = g.Sum(x => x.Quantity)
                        };

            var result = await query.FirstOrDefaultAsync();

            if (result == null)
            {
                return new countProduct
                {
                    Idproduct = id,
                    TotalQuantity = 0
                };
            }
            return result;
        }
        public async Task<BaseQuerieResponse<GetDetail_warehouseDto>>Search(Paging paging)
        {

            var query = from a in _DbContext.Detail_warehouse
                        join b in _DbContext.Products on a.Idproduct equals b.Id
                        join c in _DbContext.Size on a.Idsize equals c.Id
                        where(string.IsNullOrEmpty(paging.Keyword)||b.Name.Contains(paging.Keyword)) 
                        select new GetDetail_warehouseDto
                        {
                            Id = a.Id,
                            Idproduct = a.Idproduct,
                            NameProduct = b.Name,
                            Image = b.Image,
                            Idsize = a.Idsize,
                            NameSize = c.NameSize,
                            Idwarehouse=a.Idwarehouse,
                            Created = a.Created,
                            ModifiedBy = a.ModifiedBy,
                            Quantity = a.Quantity,

                        };
            if (query != null)
            {

            }

            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<GetDetail_warehouseDto>
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
