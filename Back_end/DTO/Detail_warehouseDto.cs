using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Detail_warehouseDto : BasedbDto
    {
        public int Idwarehouse {  get; set; }
        public int Idproduct { get; set; }
        public Guid Idsize { get; set; }
        public int Quantity { get; set; }
    }
}
