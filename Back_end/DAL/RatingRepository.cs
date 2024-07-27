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

        public async Task<BaseQuerieResponse<RatingDto>> Search(Paging paging)
        {
            var query = from a in _DbContext.Rating
                        join b in _DbContext.User on a.Id_customer equals b.Id
                        join c in _DbContext.Products on a.Id_product equals c.Id
                        where(string.IsNullOrEmpty(paging.Keyword)|| a.Status.ToString()== paging.Keyword 
                        || a.Evaluate.ToString()==paging.Keyword || b.UserName==paging.Keyword) 
                        select new RatingDto
                        {
                            Id = a.Id,
                            Id_product = a.Id_product,
                            Id_customer = a.Id_customer,
                            Id_Order = a.Id_Order,
                            Comment = a.Comment,
                            Evaluate = a.Evaluate,
                            Status = a.Status,
                            Username = b.UserName,
                            Image = c.Image
                        };
            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<RatingDto>
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;

        }
        public async Task<BaseQuerieResponse<RatingDto>> GetByProduct(Guid id , int page, int pageSize)
        {
            var query = from a in _DbContext.Rating
                        join b in _DbContext.User on a.Id_customer equals b.Id
                        where a.Id_product == id && a.Status ==2

                        select new RatingDto
                        {
                            Id = a.Id,
                            Id_product = a.Id_product,
                            Id_customer = a.Id_customer,
                            Id_Order = a.Id_Order,
                            Comment = a.Comment,
                            Evaluate = a.Evaluate,
                            Status = a.Status,
                            Username = b.UserName
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
        public async Task<BaseQuerieResponse<GetRatingByEvaluate>> GetByEvaluate(int Evaluate, int page, int pageSize)
        {
            var query = from a in _DbContext.Rating
                        join b in _DbContext.User on a.Id_customer equals b.Id
                        join c in _DbContext.Products on a.Id_product equals c.Id
                        where a.Evaluate == Evaluate

                        select new GetRatingByEvaluate
                        {
                            Id = a.Id,
                            Id_product = a.Id_product,
                            Id_customer = a.Id_customer,
                            Id_Order = a.Id_Order,
                            Comment = a.Comment,
                            Evaluate = a.Evaluate,
                            Status = a.Status,
                            Username = b.UserName,
                            Product_name=c.Name,
                            Image=c.Image,
                        };
            var totalCount = await query.LongCountAsync();
            var pageResults = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var searchResults = new BaseQuerieResponse<GetRatingByEvaluate>
            {
                PageIndex = page,
                PageSize = pageSize,
                TotalFilter = totalCount,
                Data = pageResults
            };
            return searchResults;

        }
        public async Task<CreateRatingDto> CreateS(CreateRatingDto entities)
        {
            foreach (var item in entities.ListRating)
            {
                Rating ratingDto = new Rating
                {
                    Id_Order = item.Id_Order,
                    Id_customer= item.Id_customer,
                    Id_product= item.Id_product,
                    Evaluate=item.Evaluate, 
                    Status = item.Status,
                    Comment = item.Comment,
                    Created = DateTime.Now,
                };

                await _DbContext.Rating.AddAsync(ratingDto);
                await _DbContext.SaveChangesAsync();
            }

            return entities;
        }
       

    }
}
