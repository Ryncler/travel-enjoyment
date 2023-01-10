using BaseService.AuditLogging.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace BaseService.AuditLogging
{
    public interface IAuditLogAppService : IReadOnlyAppService<AuditLogDto, Guid, GetAuditLogListDto>
    {
    }
}
