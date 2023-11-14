using Back_end.Model;
using BUS.Interface;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BUS
{
    public class CategoriesBus : GenericBus<Categories>, ICategoriesBus
    {
       
        public CategoriesBus(ICategoriesRepository categoriesRepository) : base(categoriesRepository)
        {
        }
    }
}
