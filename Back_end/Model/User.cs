using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User:IdentityUser
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? Img { get; set; }
        public int? ActiveFlag { get; set; } 
        public int? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
