
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Detail_warehouse : Basedb
    {
        public Guid Idwarehouse {  get; set; }
        public Guid Idproduct { get; set; }
        public Guid Idsize { get; set; }
        public int Quantity { get; set; }
    }
}
