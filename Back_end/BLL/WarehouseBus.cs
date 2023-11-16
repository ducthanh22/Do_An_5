using BUS;
using DAL.Interface;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class WarehouseBus : GenericBus<Warehouse>, IWarehouseBus
    {
        public IWarehouseRepository _res;
        public WarehouseBus(IWarehouseRepository res) : base(res)
        {
            _res = res;
        }
        public async Task<BaseQuerieResponse<WarehouseDto>>Search(string keywork, int page, int pageSize)
        {
            return await _res.Search(keywork, page, pageSize);
        }
    }
}
