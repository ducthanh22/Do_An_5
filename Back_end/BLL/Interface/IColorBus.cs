using System;
using System.Collections.Generic;
using Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL.Interface
{
    public interface IColorBus :IGenericBUS<Color>
    {
        Task<BaseQuerieResponse<ColorDto>> Search(Paging paging);
    }
}
