using BaseService.OpenIddicts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BaseService.OpenIddicts
{
    public interface IOpenIddictManageAppService : IApplicationService
    {
        Task<OpenIddictScopeDto> GetScopeAsync(string id);

        Task<PagedResultDto<OpenIddictScopeDto>> GetAllScopeAsync(PageListAndSortedRequestDto input);

        Task<OpenIddictScopeDto> CreateScopeAsync(OpenIddictScopeCreateDto input);

        Task<OpenIddictScopeDto> UpdateScopeAsync(string id, OpenIddictScopeUpdateDto input);

        Task DeleteScopeAsync(string id);


        Task<OpenIddictApplicationDto> GetApplicationAsync(string id);

        Task<PagedResultDto<OpenIddictApplicationDto>> GetAllApplicationAsync(PageListAndSortedRequestDto input);

        Task<OpenIddictApplicationDto> CreateApplicationAsync(OpenIddictApplicationCreateUpdateDto input);

        Task<OpenIddictApplicationDto> UpdateApplicationAsync(string id, OpenIddictApplicationCreateUpdateDto input);

        Task DeleteApplicationAsync(string id);

        List<string> GetGrantTypes();

        List<string> GetClientTypes();

        List<string> GetConsentTypes();
    }
}
