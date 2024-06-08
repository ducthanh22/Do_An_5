using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SaleDto:BasedbDto
    {
        public Guid IdProduct { get; set; }
        public int SalePrice { get; set; }
        public int percent { get; set; }
        public int SaleTime { get; set; }
    }
    public class GetSaleDto : BasedbDto
    {
        public Guid IdProduct { get; set; }
        public int? SalePrice { get; set; }
        public int? percent { get; set; }
        public int? SaleTime { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Price_product { get; set; }
    }
}
