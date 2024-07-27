
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IDetail_exportbillRepository : IGenericRepository<Detail_exportbill>
    {
        Task<countProduct> CountProduct(Guid id);
        Task<List<GetDetail_exportbillDto>> GETBYID(Guid id);
    }
}
