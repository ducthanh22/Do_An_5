using BLL.Interface;
using DAL.Interface;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SaleBus :GenericBus<Sale>,ISaleBus
    {
        public ISaleRepository _res;

        public SaleBus(ISaleRepository res) : base(res) {
            _res = res;
        }
        public Task<string> UpdateSalesPrices()
        {
            return _res.UpdateSalesPrices();
        }
        public async Task<List<GetSaleDto>> GetSale()
        {
            return await _res.GetSale();
        }
    }
}
