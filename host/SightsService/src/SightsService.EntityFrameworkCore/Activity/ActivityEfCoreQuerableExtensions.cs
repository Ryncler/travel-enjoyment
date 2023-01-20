using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SightsService.ActivityManage;

public static class ActivityEfCoreQueryableExtensions
{
    public static IQueryable<Activity> IncludeDetails(this IQueryable<Activity> queryable, bool include = true)
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
