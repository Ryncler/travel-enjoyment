using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SightsService.SightsManage;

public static class SightsTravelsEfCoreQueryableExtensions
{
    public static IQueryable<SightsTravels> IncludeDetails(this IQueryable<SightsTravels> queryable, bool include = true)
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
