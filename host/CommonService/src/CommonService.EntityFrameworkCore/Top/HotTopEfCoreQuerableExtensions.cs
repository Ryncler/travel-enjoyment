using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CommonService.Top;

public static class HotTopEfCoreQueryableExtensions
{
    public static IQueryable<HotTop> IncludeDetails(this IQueryable<HotTop> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            // .Include(x => x.xxx) // TODO: AbpHelper generated
            ;
    }
}
