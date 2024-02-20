using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ColorDto
    {
        public int IdProduct { get; set; }
        public string NameColor { get; set; }
        public string Image { get; set; }
    }
    public class UpLoadFile
    {
        public int Id { get; set; }
        public IFormFile Img { get; set; }
        public string? Image { get; set; }

    }
}
