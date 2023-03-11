using System;
using System.Linq;
using System.Threading.Tasks;
using SightsService.EntityFrameworkCore;
using SightsService.TravelsManage;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SightsService.TravelsManage;

public class UserChoiceTravelsRepository : EfCoreRepository<ISightsServiceDbContext, UserChoiceTravels, Guid>, IUserChoiceTravelsRepository
{
    public UserChoiceTravelsRepository(IDbContextProvider<ISightsServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<UserChoiceTravels>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}