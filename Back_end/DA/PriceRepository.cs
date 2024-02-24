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
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace DAL
{
    public class PriceRepository : GenericRepository<Price>, IPriceRepository
    {
        public PriceRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<BaseQuerieResponse<PriceDto>> Search(int? min, int? max, int page, int pageSize)
        {

            var query = from d in _DbContext.Set<Price>()
                        join a in _DbContext.Set<Products>() on d.Idproduct equals a.Id
                        join b in _DbContext.Set<Color>() on d.Idproduct equals b.IdProduct

                        where (d.Price_product >= min && d.Price_product <= max)
                        select new PriceDto
                        {
                            Id = d.Id,
                            Price_product=d.Price_product,
                            Idproduct=d.Idproduct,
                            Image = b.Image,
                            Name = a.Name,
                            Created=d.Created,
                            Modified=d.Modified,

                        };

            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<PriceDto>
            {
                PageIndex = page,
                PageSize = pageSize,
                Keynumber = max,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;
        }
        public async Task<Price> DeleteIdProduct(int id)
        {
            var priceToDelete = await _DbContext.Set<Price>().FirstOrDefaultAsync(p => p.Idproduct == id);
            if (priceToDelete != null)
            {
                _DbContext.Set<Price>().Remove(priceToDelete);
                await _DbContext.SaveChangesAsync();
            }
            return priceToDelete;
        }
       
                        
}
}
