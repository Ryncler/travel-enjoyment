using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Tag>> GetListByIds(List<string> ids)
    {
        var db = await GetDbContextAsync();

        var result = ids.Select(item =>
        {
            return db.Tags.Where(x => x.Id.Equals(Guid.Parse(item))).FirstOrDefault();
        }).ToList();
        return result;
    }

}