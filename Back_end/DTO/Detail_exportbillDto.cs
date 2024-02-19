using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Detail_exportbillDto : BasedbDto
    {
        public int Idproduct { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
