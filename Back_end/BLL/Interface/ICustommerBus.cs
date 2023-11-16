using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Interface
{
    public interface ICustommerBus : IGenericBUS<Custommer>
    {
        Task<BaseQuerieResponse<CustommerDto>> Search(string keywork, int page, int pageSize);

    }
}
