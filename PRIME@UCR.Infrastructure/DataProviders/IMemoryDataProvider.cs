using System.Collections.Generic;

namespace PRIME_UCR.Infrastructure.DataProviders
{
    public interface IMemoryDataProvider<T, TKey> where T : class
    {
        IDictionary<TKey, T> Data { get; set; }
    }
}