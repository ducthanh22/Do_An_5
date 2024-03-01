
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Importbill:Basedb
    {
        public int Price { get; set; }
        public int Status { get; set; }
        public Guid IdStaff { get; set; }
    }
}
