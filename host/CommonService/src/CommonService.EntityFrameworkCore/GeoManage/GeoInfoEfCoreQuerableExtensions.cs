using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CommonService.GeoManage;

public static class GeoInfoEfCoreQueryableExtensions
{
    public static IQueryable<GeoInfo> IncludeDetails(this IQueryable<GeoInfo> queryable, bool include = true)
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
