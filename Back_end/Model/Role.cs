using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Role : IdentityRole
    {
        public int? ActiveFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}
