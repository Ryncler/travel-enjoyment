using System;
using System.Linq;
using System.Threading.Tasks;
using CommonService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CommonService.User;

public class UserTravelRepository : EfCoreRepository<ICommonServiceDbContext, UserTravel, Guid>, IUserTravelRepository
{
    public UserTravelRepository(IDbContextProvider<ICommonServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<UserTravel>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}