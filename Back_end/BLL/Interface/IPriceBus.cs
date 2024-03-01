﻿
using BUS.Interface;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IPriceBus : IGenericBUS<Price>
    {
        Task<Price> DeleteIdProduct(Guid id);
        Task<BaseQuerieResponse<PriceDto>> Search(int? min, int? max, int page, int pageSize);

    }
}
