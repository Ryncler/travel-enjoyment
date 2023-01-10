using BaseService.AuditLogging.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AuditLogging;

namespace BaseService.AuditLogging
{
    public class AuditLogAppService : BaseServiceAppService, IAuditLogAppService
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLogAppService(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task<AuditLogDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<AuditLog, AuditLogDto>(await _auditLogRepository.GetAsync(id));
        }

        public async Task<PagedResultDto<AuditLogDto>> GetListAsync(GetAuditLogListDto input)
        {
            var count = await _auditLogRepository.GetCountAsync(
                startTime: input.StartTime,
                endTime: input.EndTime,
                httpMethod: input.HttpMethod,
                url: input.Url,
                userName: input.UserName,
                applicationName: input.ApplicationName,
                correlationId: input.CorrelationId,
                maxExecutionDuration: input.MaxExecutionDuration,
                minExecutionDuration: input.MinExecutionDuration,
                hasException: input.HasException,
                httpStatusCode: input.HttpStatusCode
            );
            var list = await _auditLogRepository.GetListAsync(
                sorting: input.Sorting,
                maxResultCount: input.MaxResultCount,
                skipCount: input.SkipCount,
                startTime: input.StartTime,
                endTime: input.EndTime,
                httpMethod: input.HttpMethod,
                url: input.Url,
                userName: input.UserName,
                applicationName: input.ApplicationName,
                correlationId: input.CorrelationId,
                maxExecutionDuration: input.MaxExecutionDuration,
                minExecutionDuration: input.MinExecutionDuration,
                hasException: input.HasException,
                httpStatusCode: input.HttpStatusCode,
                includeDetails: input.IncludeDetails
            );
            return new PagedResultDto<AuditLogDto>(
                count,
                ObjectMapper.Map<List<AuditLog>, List<AuditLogDto>>(list)
            );
        }
    }
}
