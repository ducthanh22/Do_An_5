using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Detail_importbillDto : BasedbDto
    {
        public Guid IdImportbill { get; set; }
        public Guid Idproduct { get; set; }
        public Guid Idsize { get; set; }

        public int Price { get; set; }
        public int Quantity { get; set; }

    }
}
