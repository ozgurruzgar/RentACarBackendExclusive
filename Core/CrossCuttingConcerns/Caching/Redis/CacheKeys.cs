using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
    public static class CacheKeys
    {
        public static string UserIdForClaim => "UserIdForClaim";
    }
}
