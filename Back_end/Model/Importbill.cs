
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Importbill:Basedb
    { 
        public int Price {  get; set; }
        public DateTime Startday {  get; set; }
        public DateTime Endday { get; set; }
    }
}
