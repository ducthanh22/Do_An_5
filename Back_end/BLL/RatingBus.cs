using BLL.Interface;
using DAL.Interface;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RatingBus : GenericBus<Rating>, IRatingBus
    {
        public IRatingRepository _res;
        public RatingBus(IRatingRepository res) : base(res)
        {
            _res = res;
        }
        public async Task<BaseQuerieResponse<RatingDto>> GetByProduct(Guid id, int page, int pageSize)
        {
            return await _res.GetByProduct(id, page, pageSize);
        }
       
        public async Task<CreateRatingDto> CreateS(CreateRatingDto entities)
        {
            return await _res.CreateS(entities);
        }
        public async Task<BaseQuerieResponse<GetRatingByEvaluate>> GetByEvaluate(int Evaluate, int page, int pageSize)
        {
            return await _res.GetByEvaluate(Evaluate, page, pageSize);
        }
        public  async Task<BaseQuerieResponse<RatingDto>> Search(Paging paging)
        {
            return await _res.Search(paging);
        }
    }
}
