using Model;
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
    public interface IWarehouseBus : IGenericBUS<Warehouse>
    {
        Task<BaseQuerieResponse<WarehouseDto>> Search(string? keywork, int page, int pageSize);
    }
}
