using System;
using System.Linq;
using System.Threading.Tasks;
using CommonService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CommonService.CategoryManage;

public class TagRepository : EfCoreRepository<ICommonServiceDbContext, Tag, Guid>, ITagRepository
{
    public TagRepository(IDbContextProvider<ICommonServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Tag>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}