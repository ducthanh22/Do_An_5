using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserDto
    {
        
        public string Email {  get; set; }
        public string PasswordHash { get; set; }
    }
    public class GennToken
    {
        public string UserName { get; set; }
        //public string PasswordHash { get; set; }
        public string Token {  get; set; }
    }
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
    public class ForgotPasswordModel
    {
        public string Email { get; set; }
    }


}
