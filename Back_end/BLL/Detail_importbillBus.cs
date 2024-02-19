using BUS.Interface;
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
    public class Detail_importbillBus : GenericBus<Detail_importbillDto>, IDetail_importbillBus
    {
        public IDetail_importbillRepository _res;
        public Detail_importbillBus(IDetail_importbillRepository res) : base(res)
        {
            _res = res;
        }
        //public async Task<IEnumerable<BaseQuerieResponse<Detail_importbillDto>>> Search(string keywork, int page, int pageSize)
        //{
        //    return await _res.Search(keywork, page, pageSize);
        //}
    }
}
