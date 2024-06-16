
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IDetail_importbillBus : IGenericBUS<Detail_importbill>
    {
        Task<List<GetDetail_importbillDto>> GETBYID(Guid id);
    }
}
