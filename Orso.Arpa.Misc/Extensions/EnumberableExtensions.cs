using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Orso.Arpa.Misc.Extensions
{
    public static class EnumberableExtensions
    {
        public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, Task<TResult>> method,
            int concurrency = int.MaxValue)
        {
            var semaphore = new SemaphoreSlim(concurrency);
            try
            {
                return await Task.WhenAll(source.Select(async s =>
                {
                    try
                    {
                        await semaphore.WaitAsync();
                        return await method(s);
                    }
                    finally
                    {
                        _ = semaphore.Release();
                    }
                }));
            }
            finally
            {
                semaphore.Dispose();
            }
        }

        public static bool AreAllSame<T>(this IEnumerable<T> enumerable)
        {
            ArgumentNullException.ThrowIfNull(enumerable);

            using IEnumerator<T> enumerator = enumerable.GetEnumerator();
            var toCompare = default(T);
            if (enumerator.MoveNext())
            {
                toCompare = enumerator.Current;
            }

            while (enumerator.MoveNext())
            {
                if (toCompare == null && enumerator.Current != null)
                {
                    return false;
                }
                if (toCompare != null && !toCompare.Equals(enumerator.Current))
                {
                    return false;
                }

            }

            return true;
        }
    }
}
