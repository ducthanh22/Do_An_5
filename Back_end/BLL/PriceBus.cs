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
    public class PriceBus : GenericBus<Price>, IPriceBus
    {
        public IPriceRepository _res;
        public PriceBus(IPriceRepository res) : base(res)
        {
            _res = res;
        }
        public async Task<BaseQuerieResponse<PriceDto>> Search(int keywork, int page, int pageSize)
        {
            return await _res.Search(keywork, page, pageSize);
        }
    }
}
