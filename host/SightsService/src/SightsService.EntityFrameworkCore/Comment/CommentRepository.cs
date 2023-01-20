using System;
using System.Linq;
using System.Threading.Tasks;
using SightsService.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SightsService.CommentManage;

public class CommentRepository : EfCoreRepository<ISightsServiceDbContext, Comment, Guid>, ICommentRepository
{
    public CommentRepository(IDbContextProvider<ISightsServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Comment>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}