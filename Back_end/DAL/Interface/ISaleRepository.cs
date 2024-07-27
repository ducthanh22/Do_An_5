using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ISaleRepository: IGenericRepository<Sale>
    {
        Task<int> UpdateSalesPrices();
        Task<List<GetSaleDto>> GetSale(string? keyword, int active);
        Task<List<SaleDto>> CREATE(List<SaleDto> dto);
    }
}
