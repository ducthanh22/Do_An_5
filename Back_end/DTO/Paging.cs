using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Paging
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } 
        public string? Keyword { get; set; } = "";
        public string? OrderBy { get; set; }
    }
}
