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
    }
}
