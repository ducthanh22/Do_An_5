using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DTO;

namespace BLL.Interface
{
    public interface ICategoriesBus : IGenericBUS<Categories>
    {
        Task<BaseQuerieResponse<CategoriesDto>> Search(Paging paging);

    }
}
