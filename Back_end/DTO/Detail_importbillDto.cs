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
        public Guid IdImportbillId { get; set; }
        public Guid Idproduct { get; set; }
        public Guid Idsize { get; set; }

        public int Price { get; set; }
        public int Quantity { get; set; }

    }
    public class GetDetail_importbillDto : BasedbDto
    {
        public Guid IdImportbillId { get; set; }

        public Guid Idproduct { get; set; }
        public Guid Idsize { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string productName { get; set; }
        public string image { get; set; }
        public string userName { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public int toTal { get; set; }

    }
}
