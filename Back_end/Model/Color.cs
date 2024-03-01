
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Color:Basedb
    {
        public Guid IdProduct { get; set; }
        public string NameColor { get; set; }
        public string Image { get; set; }
    }
}
