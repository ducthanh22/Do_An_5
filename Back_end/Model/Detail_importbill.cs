using Back_end.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Detail_importbill : Basedb
    {
        public int IdImportbillId { get; set; }
        public int Idproduct {  get; set; }
        public int Price {  get; set; }
        public int Quantity {  get; set; }
    
    }
}
