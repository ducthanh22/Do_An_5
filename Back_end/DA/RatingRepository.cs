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
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        public RatingRepository(Achino_DbContext dbContext, IMapper mapper) : base(dbContext, mapper) {
        }

        public async Task<BaseQuerieResponse<RatingDto>> GetByProduct(Guid id , int page, int pageSize)
        {
            var query = from a in _DbContext.Rating
                        where a.Id_product == id
                        select new RatingDto
                        {
                            Id = a.Id,
                            Id_product = a.Id_product,
                            Id_customer = a.Id_customer,
                            Id_Order = a.Id_Order,
                            Comment = a.Comment,
                            Evaluate = a.Evaluate,
                            Status = a.Status
                        };
            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<RatingDto>
            {
                PageIndex = page,
                PageSize = pageSize,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;

        }
        
    }
}
