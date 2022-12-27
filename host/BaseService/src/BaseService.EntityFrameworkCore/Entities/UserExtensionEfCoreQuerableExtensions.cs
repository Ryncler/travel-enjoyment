using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BaseService.Entities;

public static class UserExtensionEfCoreQueryableExtensions
{
    public static IQueryable<UserExtension> IncludeDetails(this IQueryable<UserExtension> queryable, bool include = true)
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
