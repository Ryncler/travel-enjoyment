using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StorageService.Storage;

public static class ImageEfCoreQueryableExtensions
{
    public static IQueryable<Image> IncludeDetails(this IQueryable<Image> queryable, bool include = true)
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
