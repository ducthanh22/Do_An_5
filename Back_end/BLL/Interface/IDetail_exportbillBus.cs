
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IDetail_exportbillBus : IGenericBUS<Detail_exportbill>
    {
        Task<countProduct> CountProduct(Guid id);
        Task<List<GetDetail_exportbillDto>> GETBYID(Guid id);

    }
}
