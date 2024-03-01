
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Detail_importbill : Basedb
    {
        public Guid IdImportbillId { get; set; }
        public Guid Idproduct {  get; set; }
        public int Price {  get; set; }
        public int Quantity {  get; set; }
    
    }
}
