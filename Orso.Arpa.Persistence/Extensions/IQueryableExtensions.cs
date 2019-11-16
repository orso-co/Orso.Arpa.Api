using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Orso.Arpa.Persistence.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<bool> ExistsAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            return (await source.SingleOrDefaultAsync(predicate, cancellationToken)) != null;
        }
    }
}
