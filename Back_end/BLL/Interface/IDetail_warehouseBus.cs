
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IDetail_warehouseBus :IGenericBUS<Detail_warehouse>
    {
        Task<countProduct> CountProduct(Guid id, Guid idSize);
        Task<BaseQuerieResponse<GetDetail_warehouseDto>> Search(Paging paging);

    }
}
