using BaseService.AuditLogging;
using BaseService.AuditLogging.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace BaseService.Entities
{
    [Area(BaseServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = BaseServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/audit-logging/auditlog")]
    public class AuditLogController : BaseServiceController, IAuditLogAppService
    {
        private readonly IAuditLogAppService _auditLogAppService;

        public AuditLogController(IAuditLogAppService auditLogAppService)
        {
            _auditLogAppService = auditLogAppService;
        }

        [HttpGet]
        [Route("get")]
        public Task<AuditLogDto> GetAsync(Guid id)
        {
            return _auditLogAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<AuditLogDto>> GetListAsync(GetAuditLogListDto input)
        {
            return _auditLogAppService.GetListAsync(input);
        }
    }
}
