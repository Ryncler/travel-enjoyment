using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SightsService.TravelsManage;

public static class TravelsEfCoreQueryableExtensions
{
    public static IQueryable<Travels> IncludeDetails(this IQueryable<Travels> queryable, bool include = true)
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
