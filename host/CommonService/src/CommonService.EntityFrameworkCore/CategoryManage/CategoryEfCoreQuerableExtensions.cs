using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CommonService.CategoryManage;

public static class CategoryEfCoreQueryableExtensions
{
    public static IQueryable<Category> IncludeDetails(this IQueryable<Category> queryable, bool include = true)
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
