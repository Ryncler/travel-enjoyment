using System;
using System.Linq;
using System.Threading.Tasks;
using SightsService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SightsService.ActivityManage;

public class ActivityRepository : EfCoreRepository<ISightsServiceDbContext, Activity, Guid>, IActivityRepository
{
    public ActivityRepository(IDbContextProvider<ISightsServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Activity>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}