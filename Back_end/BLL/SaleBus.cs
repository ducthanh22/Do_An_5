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
        public Task<int> UpdateSalesPrices()
        {
            return _res.UpdateSalesPrices();
        }
        public async Task<List<GetSaleDto>> GetSale(string? keyword, int active)
        {
            return await _res.GetSale(keyword,active);
        }
        public async Task<List<SaleDto>> CREATE(List<SaleDto> dto)
        {
            return await _res.CREATE(dto);
        }
    }
}
