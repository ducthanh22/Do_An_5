using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ClaimDto
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class RoleDto
    {
        public string Name { get; set; }
    }

    public class CreateRoleDto
    {
        public RoleDto Role { get; set; }
        public List<ClaimDto> RoleClaims { get; set; }
    }

}
