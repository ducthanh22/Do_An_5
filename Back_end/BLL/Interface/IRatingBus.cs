using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IRatingBus :IGenericBUS<Rating>
    {
        Task<BaseQuerieResponse<RatingDto>> GetByProduct(Guid id, int page, int pageSize);

    }
}
