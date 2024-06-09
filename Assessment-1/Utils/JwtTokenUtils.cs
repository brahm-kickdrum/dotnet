using Assessment_1.Enums;
using System.Security.Claims;

namespace Assessment_1.Utils
{
    public static class JwtTokenUtils
    {
        public static string? ExtractEmail(HttpContext httpContext)
        {
            return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }

        public static UserRole? ExtractRole(HttpContext httpContext)
        {
            string? roleName = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (Enum.TryParse<UserRole>(roleName, out UserRole role))
            {
                return role;
            }

            return null;
        }
    }
}
