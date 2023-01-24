using System;
using System.Linq;
using System.Threading.Tasks;
using CommonService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CommonService.CategoryManage;

public class CategoryRepository : EfCoreRepository<ICommonServiceDbContext, Category, Guid>, ICategoryRepository
{
    public CategoryRepository(IDbContextProvider<ICommonServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Category>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}