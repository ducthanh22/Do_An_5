using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProducesDto :BasedbDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; } = "";

    }
    public class UpFile
    {
        public int Id { get; set; }
        public IFormFile Img { get; set; }
        public string? Image { get; set; }

    }
}
