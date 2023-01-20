using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SightsService.TravelsManage;

public static class TravelsExtentionEfCoreQueryableExtensions
{
    public static IQueryable<TravelsExtention> IncludeDetails(this IQueryable<TravelsExtention> queryable, bool include = true)
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
