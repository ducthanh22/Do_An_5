using BUS;
using BUS.Interface;
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
    public class Detail_warehouseBus : GenericBus<Detail_warehouse>, IDetail_warehouseBus
    {
        public IDetail_warehouseRepository _res;
        public Detail_warehouseBus(IDetail_warehouseRepository res) : base(res)
        {
            _res = res;
        }
        //public async Task<IEnumerable<BaseQuerieResponse<Detail_warehouseDto>>> Search(string keywork, int page, int pageSize)
        //{
        //    return await _res.Search(keywork, page, pageSize);
        //}
    }
}
