using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        Task<object> Get(string key);
        Task Add(string key, RedisValue value);
        Task<bool> IsAdd(string key);
        Task Remove(string key);
    }
}
