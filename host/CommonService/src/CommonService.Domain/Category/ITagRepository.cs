using System;
using Volo.Abp.Domain.Repositories;

namespace CommonService.CategoryManage;

public interface ITagRepository : IRepository<Tag, Guid>
{
}
