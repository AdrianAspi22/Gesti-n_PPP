using GestionAsesoria.Operator.Application.Wrappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Infrastructure.Persistence.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResponse<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize)
            where T : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            long count = await source.LongCountAsync();
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            List<T> items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResponse<T>(items, count, pageIndex, pageSize);
        }
        public static async Task<ListedResponse<T>> ToListedAsync<T>(this IQueryable<T> source)
            where T : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            List<T> items = await source.ToListAsync();
            return new ListedResponse<T>(items);
        }
    }
}
