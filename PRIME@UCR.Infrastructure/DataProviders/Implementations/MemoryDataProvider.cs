using System.Collections.Generic;

namespace PRIME_UCR.Infrastructure.DataProviders.Implementations
{
    public class MemoryDataProvider<T, TKey> : IMemoryDataProvider<T, TKey> where T : class
    {
        public IDictionary<TKey, T> Data { get; set; }
        public MemoryDataProvider()
        {
            Data = new Dictionary<TKey, T>();
        }
    }
}