
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IDetail_warehouseRepository : IGenericRepository<Detail_warehouseDto>
    {
        //Task<IEnumerable<BaseQuerieResponse<Detail_warehouseDto>>> Search(string keywork, int page, int pageSize);
    }
}
