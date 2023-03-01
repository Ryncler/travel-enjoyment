using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SightsService.SightsManage;

public static class SightsTagEfCoreQueryableExtensions
{
    public static IQueryable<SightsTag> IncludeDetails(this IQueryable<SightsTag> queryable, bool include = true)
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
