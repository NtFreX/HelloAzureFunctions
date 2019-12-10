using System.Collections.Generic;
using System.Threading.Tasks;

namespace NtFreX.HelloAzureFunctions.Extensions {
    public static class AsyncEnumerableExtensions {
        public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> values) {
            var list = new List<T>();
            await foreach(var value in values) {
                list.Add(value);
            }
            return list;
        }
    }
}