using BaseService.OpenIddicts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace BaseService.OpenIddicts
{
    public interface IOpenIddictManageAppService : IApplicationService
    {
        Task<OpenIddictScopeDto> GetScopeAsync(string id);

        Task<List<OpenIddictScopeDto>> GetAllScopeAsync(OpenIddictGetListInputDto input);

        Task<OpenIddictScopeDto> CreateScopeAsync(OpenIddictScopeCreateDto input);

        Task<OpenIddictScopeDto> UpdateScopeAsync(string id,OpenIddictScopeUpdateDto input);

        Task DeleteScopeAsync(string id);


        Task<OpenIddictApplicationDto> GetApplicationAsync(string id);

        Task<List<OpenIddictApplicationDto>> GetAllApplicationAsync(OpenIddictGetListInputDto input);

        Task<OpenIddictApplicationDto> CreateApplicationAsync(OpenIddictApplicationCreateUpdateDto input);

        Task<OpenIddictApplicationDto> UpdateApplicationAsync(string id, OpenIddictApplicationCreateUpdateDto input);

        Task DeleteApplicationAsync(string id);
    }
}
