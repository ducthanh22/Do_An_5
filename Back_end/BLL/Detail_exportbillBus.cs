using BUS.Interface;
using DAL;
using DAL.Interface;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class Detail_exportbillBus : GenericBus<Detail_exportbill>, IDetail_exportbillBus
    {
        public IDetail_exportbillRepository _res;
        public Detail_exportbillBus(IDetail_exportbillRepository res) : base(res)
        {
            _res = res;
        }
        //public async Task<BaseQuerieResponse<Detail_exportbillDto>> Search(string keywork, int page, int pageSize)
        //{
        //    return await _res.Search(keywork, page, pageSize);
        //}
    }
}
