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
        Task<string> UpdateSalesPrices();
        Task<List<GetSaleDto>> GetSale();
    }
}
