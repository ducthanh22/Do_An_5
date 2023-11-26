using DTO.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Model;
using System.Text.RegularExpressions;

namespace Back_end.Attribute
{
    public class HasPermissionAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly int[] _modules;
        private readonly int[] _permissions;

        public HasPermissionAttribute(int[] modules, int[] permissions)
        {
            _modules = modules;
            _permissions = permissions;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                // Lấy token từ headers
                var token = GetToken(context);

                if (string.IsNullOrEmpty(token))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                var handler = new JwtSecurityTokenHandler();
                var decodedToken = handler.ReadJwtToken(token);
                
                var isAccess = decodedToken.Claims.Any(x => _modules.Select
                (m => m.ToString()).Contains(x.Type) && _permissions.All(p => int.TryParse(x.Value, out int intValue) && intValue == p));

                //var isAccess = decodedToken.Claims.Any(x => _modules.Select(m => m.ToString()).Contains(x.Type) && _permissions.All(p => JsonExtensions.DeserializeFromJson<List<int>>(x.Value).Contains(p)));
                // Tiếp tục kiểm tra quyền truy cập bình thường
                if (!isAccess)
                    context.Result = new ForbidResult(); // Forbidden
            }
            catch (SecurityTokenException)
            {
                context.Result = new UnauthorizedResult(); // Unauthorized
            }


        }

        private static string? GetToken(AuthorizationFilterContext context)
        {
            var token = string.Empty;
            var auth = context.HttpContext.Request.Headers["Authorization"].ToString();
            token = string.IsNullOrWhiteSpace(auth) switch
            {
                false when auth != null && auth.ToLower().Contains("bearer") => Regex.Replace(auth, "bearer |Bearer |bearer_|Bearer_",
                    ""),
                false => auth,
                _ => token
            };

            return token;
        }

    }
}