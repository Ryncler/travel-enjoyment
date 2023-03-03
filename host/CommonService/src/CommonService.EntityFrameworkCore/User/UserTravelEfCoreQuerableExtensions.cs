using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CommonService.User;

public static class UserTravelEfCoreQueryableExtensions
{
    public static IQueryable<UserTravel> IncludeDetails(this IQueryable<UserTravel> queryable, bool include = true)
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
