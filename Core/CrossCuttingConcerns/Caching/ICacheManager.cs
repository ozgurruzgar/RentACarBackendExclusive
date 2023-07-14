using Castle.DynamicProxy;
using Core.Entities;
using Core.Utilities.Results;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        Task<SuccessDataResult<TResponse>> Get<TResponse>(string key);
        Task Add(string key, object value);
        bool IsAdd(string key);
        Task Remove(string key);

    }
}
