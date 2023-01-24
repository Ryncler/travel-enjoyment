using System;
using Volo.Abp.Domain.Repositories;

namespace CommonService.GeoManage;

public interface IGeoInfoRepository : IRepository<GeoInfo, Guid>
{
}
