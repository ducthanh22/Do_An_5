using Back_end.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CustommerDto:BasedbDto
    {
        public string Name {  get; set; }
        public string Sdt { get; set; }
        public string Email {  get; set; }
        public string Address { get; set; }
    }
}
