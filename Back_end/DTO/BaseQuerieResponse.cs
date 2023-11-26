using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BaseQuerieResponse<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; } 
        public string? Keyword { get; set; } = "";
        public int? Keynumber { get; set; } 
        public long TotalFilter { get; set; }
        public List<T> Data { get; set; }
    }
}
