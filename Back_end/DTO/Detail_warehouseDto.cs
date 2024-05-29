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
        public Guid Idwarehouse { get; set; }
        public Guid Idproduct { get; set; }
        public Guid Idsize { get; set; }
        public int Quantity { get; set; }
    }
    public class GetDetail_warehouseDto : BasedbDto
    {
        public Guid Idwarehouse { get; set; }
        public Guid Idproduct { get; set; }
        public string NameProduct { get; set; }
        public string Image { get; set; }
        public Guid Idsize { get; set; }
        public string NameSize { get; set; }
        public int Quantity { get; set; }
    }
}
