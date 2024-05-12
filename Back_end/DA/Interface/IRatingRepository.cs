using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        Task<BaseQuerieResponse<RatingDto>> GetByProduct(Guid id, int page, int pageSize);
        Task<CreateRatingDto> CreateS(CreateRatingDto entities);

    }
}
