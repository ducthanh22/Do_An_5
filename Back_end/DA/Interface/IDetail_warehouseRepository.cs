
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IDetail_warehouseRepository : IGenericRepository<Detail_warehouse>
    {
        Task<countProduct> CountProduct(Guid id);
        Task<BaseQuerieResponse<GetDetail_warehouseDto>> Search(Paging paging);
    }
}
