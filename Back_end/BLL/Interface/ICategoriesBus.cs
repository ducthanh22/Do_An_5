﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Back_end.Model;
using DTO;

namespace BUS.Interface
{
    public interface ICategoriesBus : IGenericBUS<Categories>
    {
        Task<BaseQuerieResponse<CategoriesDto>> Search(string keywork, int page, int pageSize);

    }
}
