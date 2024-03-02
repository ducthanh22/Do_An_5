using Model;
using BLL.Interface;
using DAL.Interface;
using DTO;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BLL
{
    public class CategoriesBus : GenericBus<Categories>, ICategoriesBus
    {

        private readonly ICategoriesRepository _res;
        public CategoriesBus(ICategoriesRepository res) : base(res)
        {
            _res = res;
        }

        public async Task<BaseQuerieResponse<CategoriesDto>> Search(Paging paging)
        {
            return await _res.Search(paging);
        }
    }
}
