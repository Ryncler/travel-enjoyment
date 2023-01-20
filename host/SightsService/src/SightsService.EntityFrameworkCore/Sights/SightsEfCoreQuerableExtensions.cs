using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SightsService.SightsManage;

public static class SightsEfCoreQueryableExtensions
{
    public static IQueryable<Sights> IncludeDetails(this IQueryable<Sights> queryable, bool include = true)
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
