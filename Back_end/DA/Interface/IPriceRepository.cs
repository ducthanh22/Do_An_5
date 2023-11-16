﻿using Back_end.Model;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IPriceRepository: IGenericRepository<Price>
    {
        Task<BaseQuerieResponse<PriceDto>> Search(int keywork, int page, int pageSize);
    }
}
