﻿using Back_end.Model;
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
    public interface IExportbillBus :IGenericBUS<Exportbill>
    {
        Task<BaseQuerieResponse<ExportbillDto>> Search(int keywork, int page, int pageSize);
    }
}
