using System;
using Volo.Abp.Domain.Repositories;

namespace SightsService.ActivityManage;

public interface IActivityRepository : IRepository<Activity, Guid>
{
}
