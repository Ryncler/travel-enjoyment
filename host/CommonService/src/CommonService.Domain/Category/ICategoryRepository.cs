using System;
using Volo.Abp.Domain.Repositories;

namespace CommonService.CategoryManage;

public interface ICategoryRepository : IRepository<Category, Guid>
{
}
