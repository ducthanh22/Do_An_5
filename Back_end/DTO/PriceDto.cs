using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PriceDto : BasedbDto
    {
        public int Idproduct { get; set; }
        public int Price_product { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }



    }
}
