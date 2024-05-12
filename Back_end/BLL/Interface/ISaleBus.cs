using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ISaleBus :IGenericBUS<Sale>
    {
        Task<string> UpdateSalesPrices();
        Task<List<GetSaleDto>> GetSale();
    }
}
