using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace CommonService.CategoryManage;

public interface ITagRepository : IRepository<Tag, Guid>
{
    Task<List<Tag>> GetListByIds(List<string> ids);
}
