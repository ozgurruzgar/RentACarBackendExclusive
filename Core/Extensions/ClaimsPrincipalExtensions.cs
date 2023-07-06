using System.Security.Claims;
using System.Linq;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            List<string> list = claimsPrincipal?.Claims(ClaimTypes.Role);
            if(list.Count == 0)
            {
                throw new Exception("Claims metodu boş dönüyor.");
            }
            return list;
        }
    }
}
