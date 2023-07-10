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
        Task Add(string key, byte[] value);
        bool IsAdd(string key);
        Task Remove(string key);

    }
}
