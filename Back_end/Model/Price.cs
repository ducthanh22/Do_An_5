
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Price : Basedb
    {
        public Guid Idproduct {  get; set; }
        public int ?Price_product {  get; set; }
        
    }
}
