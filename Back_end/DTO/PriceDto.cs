using Back_end.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PriceDto : BasedbDto
    {
        public int Idproduct {  get; set; }
        public int Price_product {  get; set; }
        public DateTime Startday { get; set; } = DateTime.Now;
        public DateTime Endday { get; set; } = DateTime.Now;

    }
}
