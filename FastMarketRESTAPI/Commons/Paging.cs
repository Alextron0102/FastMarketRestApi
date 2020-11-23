using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Commons
{
    public static class Paging
    {
        public static async Task<DataCollection<T>> PagedAsync<T>(
            this IQueryable<T> query,
            int page,
            int take
        ) where T : class
        {
            var result = new DataCollection<T>();

            result.Total = await query.CountAsync();
            result.Page = page;

            if (result.Total > 0)
            {
                result.Pages = Convert.ToInt32(
                    Math.Ceiling(
                        Convert.ToDecimal(result.Total) / take
                    )
                );
                if (page > 0)
                {
                    result.Items = await query.Skip((page - 1) * take)
                                                .Take(take)
                                                .ToListAsync();
                }
                else return result;
            }
            return result;
        }
    }
}
