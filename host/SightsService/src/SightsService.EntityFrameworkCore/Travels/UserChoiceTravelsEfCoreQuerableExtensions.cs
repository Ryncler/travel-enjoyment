using System.Linq;
using Microsoft.EntityFrameworkCore;
using SightsService.TravelsManage;

namespace SightsService.TravelsManage;

public static class UserChoiceTravelsEfCoreQueryableExtensions
{
    public static IQueryable<UserChoiceTravels> IncludeDetails(this IQueryable<UserChoiceTravels> queryable, bool include = true)
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
