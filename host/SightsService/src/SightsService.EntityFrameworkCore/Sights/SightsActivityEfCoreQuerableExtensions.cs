using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SightsService.SightsManage;

public static class SightsActivityEfCoreQueryableExtensions
{
    public static IQueryable<SightsActivity> IncludeDetails(this IQueryable<SightsActivity> queryable, bool include = true)
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
