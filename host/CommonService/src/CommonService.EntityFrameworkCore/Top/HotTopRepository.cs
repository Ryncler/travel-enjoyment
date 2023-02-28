using System;
using System.Linq;
using System.Threading.Tasks;
using CommonService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CommonService.Top;

public class HotTopRepository : EfCoreRepository<ICommonServiceDbContext, HotTop, Guid>, IHotTopRepository
{
    public HotTopRepository(IDbContextProvider<ICommonServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<HotTop>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}