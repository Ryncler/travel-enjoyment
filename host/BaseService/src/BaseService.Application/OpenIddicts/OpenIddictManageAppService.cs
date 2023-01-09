using BaseService.OpenIddicts.Dtos;
using OpenIddict.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.OpenIddict.Applications;
using Volo.Abp.OpenIddict.Scopes;
using Volo.Abp.Security.Encryption;

namespace BaseService.OpenIddicts
{
    public class OpenIddictManageAppService : BaseServiceAppService, IOpenIddictManageAppService
    {
        private readonly IOpenIddictScopeManager _scopeManager;
        private readonly IAbpApplicationManager _applicationManager;
        private readonly IOpenIddictApplicationRepository _applicationRepository;
        private readonly IOpenIddictScopeRepository _scopeRepository;
        private readonly IDataFilter _dataFilter;

        public OpenIddictManageAppService(IOpenIddictScopeManager scopeManager, IOpenIddictScopeRepository scopeRepository, IAbpApplicationManager applicationManager,
            IOpenIddictApplicationRepository openIddictApplicationRepository, IDataFilter dataFilter)
        {
            _dataFilter = dataFilter;
            _scopeManager = scopeManager;
            _scopeRepository = scopeRepository;
            _applicationManager = applicationManager;
            _applicationRepository = openIddictApplicationRepository;
        }

        public async Task<OpenIddictApplicationDto> CreateApplicationAsync(OpenIddictApplicationCreateUpdateDto input)
        {
            var client = await _applicationManager.FindByClientIdAsync(input.ClientId);
            if (client == null)
            {
                var application = new AbpApplicationDescriptor
                {
                    ClientId = input.ClientId,
                    ClientSecret = input.ClientSecret,
                    ClientUri = input.ClientUrl,
                    ConsentType = input.ConsentType,
                    DisplayName = input.DisplayName,
                    Type = input.Type
                };
                AddPermissionByGrantTypes(input, application);
                var buildInScopes = new[]
                {
                    OpenIddictConstants.Permissions.Scopes.Address,
                    OpenIddictConstants.Permissions.Scopes.Email,
                    OpenIddictConstants.Permissions.Scopes.Phone,
                    OpenIddictConstants.Permissions.Scopes.Profile,
                    OpenIddictConstants.Permissions.Scopes.Roles
                };
                foreach (var scope in input.Scopes)
                {
                    if (buildInScopes.Contains(scope))
                    {
                        application.Permissions.Add(scope);
                    }
                    else
                    {
                        application.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.Scope + scope);
                    }
                }
                foreach (var item in input.RedirectUrls)
                {
                    if (!item.IsNullOrEmpty())
                    {
                        if (!Uri.TryCreate(item, UriKind.Absolute, out var uri) || !uri.IsWellFormedOriginalString())
                        {
                            throw new BusinessException("这不是一个Url", item);
                        }

                        if (application.RedirectUris.All(x => x != uri))
                        {
                            application.RedirectUris.Add(uri);
                        }
                    }
                }
                foreach (var item in input.PostLogoutRedirectUrls)
                {
                    if (!item.IsNullOrEmpty())
                    {
                        if (!Uri.TryCreate(item, UriKind.Absolute, out var uri) || !uri.IsWellFormedOriginalString())
                        {
                            throw new BusinessException("这不是一个Url", item);
                        }

                        if (application.PostLogoutRedirectUris.All(x => x != uri))
                        {
                            application.PostLogoutRedirectUris.Add(uri);
                        }
                    }
                }
                var entity = (OpenIddictApplicationModel)await _applicationManager.CreateAsync(application);
                if (entity != null)
                {
                    var permissions = JsonSerializer.Deserialize<HashSet<string>>(entity.Permissions);
                    var grantTypes = GetGrantTypeByPermission(permissions);
                    var scopes = GetScopeByPermission(permissions);
                    await AddApiScopeByPermission(permissions, scopes);
                    var result = new OpenIddictApplicationDto
                    {
                        ClientId = entity.ClientId,
                        ClientSecret = entity.ClientSecret,
                        ClientUrl = entity.ClientUri,
                        ConsentType = entity.ClientUri,
                        DisplayName = entity.DisplayName,
                        Type = entity.Type,
                        GrantTypes = grantTypes,
                        Scopes = scopes,
                        RedirectUrls = JsonSerializer.Deserialize<List<string>>(entity.RedirectUris),
                        PostLogoutRedirectUrls = JsonSerializer.Deserialize<List<string>>(entity.PostLogoutRedirectUris),
                    };
                    return result;
                }
                return null;
            }
            return null;
        }

        private static void AddPermissionByGrantTypes(OpenIddictApplicationCreateUpdateDto input, AbpApplicationDescriptor application)
        {
            if (string.Equals(application.Type, OpenIddictConstants.ClientTypes.Public, StringComparison.OrdinalIgnoreCase))
            {
                application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.CodeIdTokenToken);
                application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.CodeToken);
            }
            if (input.RedirectUrls.Count > 0 || input.PostLogoutRedirectUrls.Count > 0)
            {
                application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Logout);
            }
            if (input.GrantTypes.Contains(OpenIddictConstants.GrantTypes.AuthorizationCode))
            {
                application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode);
                application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.Code);
            }
            if (input.GrantTypes.Contains(OpenIddictConstants.GrantTypes.AuthorizationCode) || input.GrantTypes.Contains(OpenIddictConstants.GrantTypes.Implicit))
            {
                application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Authorization);
            }
            if (input.GrantTypes.Contains(OpenIddictConstants.GrantTypes.AuthorizationCode) ||
                    input.GrantTypes.Contains(OpenIddictConstants.GrantTypes.ClientCredentials) ||
                    input.GrantTypes.Contains(OpenIddictConstants.GrantTypes.Password) ||
                    input.GrantTypes.Contains(OpenIddictConstants.GrantTypes.RefreshToken) ||
                    input.GrantTypes.Contains(OpenIddictConstants.GrantTypes.DeviceCode))
            {
                application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Token);
                application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Revocation);
                application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Introspection);
            }
            if (input.GrantTypes.Contains(OpenIddictConstants.GrantTypes.ClientCredentials))
            {
                application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.ClientCredentials);
            }

            if (input.GrantTypes.Contains(OpenIddictConstants.GrantTypes.Implicit))
            {
                application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.Implicit);
            }

            if (input.GrantTypes.Contains(OpenIddictConstants.GrantTypes.Password))
            {
                application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.Password);
            }

            if (input.GrantTypes.Contains(OpenIddictConstants.GrantTypes.RefreshToken))
            {
                application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.RefreshToken);
            }

            if (input.GrantTypes.Contains(OpenIddictConstants.GrantTypes.DeviceCode))
            {
                application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.DeviceCode);
                application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Device);
            }

            if (input.GrantTypes.Contains(OpenIddictConstants.GrantTypes.Implicit))
            {
                application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.IdToken);
                if (string.Equals(input.Type, OpenIddictConstants.ClientTypes.Public, StringComparison.OrdinalIgnoreCase))
                {
                    application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.IdTokenToken);
                    application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.Token);
                }
            }
        }

        public async Task<OpenIddictScopeDto> CreateScopeAsync(OpenIddictScopeCreateDto input)
        {
            if (await _scopeManager.FindByNameAsync(input.Name) == null)
            {
                var entity = ObjectMapper.Map<OpenIddictScopeCreateDto, OpenIddictScope>(input);

                await _scopeRepository.InsertAsync(entity);
                var result = ObjectMapper.Map<OpenIddictScope, OpenIddictScopeDto>(entity);
                return result;
            }
            return null;
        }

        public async Task DeleteApplicationAsync(string id)
        {
            var entity = await _applicationManager.FindByIdAsync(id);
            if (entity != null)
            {
                await _applicationManager.DeleteAsync(entity);
            }
        }

        public async Task DeleteScopeAsync(string id)
        {
            var entity = await _scopeRepository.GetAsync(Guid.Parse(id));
            if (entity != null)
            {
                await _scopeRepository.DeleteAsync(entity);
            }
        }

        public async Task<PagedResultDto<OpenIddictApplicationDto>> GetAllApplicationAsync(PageListAndSortedRequestDto input)
        {
            var result = new PagedResultDto<OpenIddictApplicationDto>();
            input.Sorting = !string.IsNullOrWhiteSpace(input.Sorting) ? input.Sorting : nameof(OpenIddictScopeDto.CreationTime);
            if (input.IsAll)
            {
                using (_dataFilter.Disable<ISoftDelete>())
                {
                    var data = new List<OpenIddictApplicationDto>();
                    var list = await _applicationRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
                    foreach (var item in list)
                    {
                        var permissions = JsonSerializer.Deserialize<HashSet<string>>(item.Permissions);
                        var grantTypes = GetGrantTypeByPermission(permissions);
                        var scopes = GetScopeByPermission(permissions);
                        await AddApiScopeByPermission(permissions, scopes);
                        data.Add(new OpenIddictApplicationDto
                        {
                            Id = item.Id,
                            ClientId = item.ClientId,
                            ClientSecret = item.ClientSecret,
                            ClientUrl = item.ClientUri,
                            ConsentType = item.ClientUri,
                            DisplayName = item.DisplayName,
                            Type = item.Type,
                            GrantTypes = grantTypes,
                            Scopes = scopes,
                            RedirectUrls = JsonSerializer.Deserialize<List<string>>(item.RedirectUris),
                            PostLogoutRedirectUrls = JsonSerializer.Deserialize<List<string>>(item.PostLogoutRedirectUris),
                            CreationTime = item.CreationTime,
                            CreatorId = item.CreatorId,
                            LastModificationTime = item.LastModificationTime,
                            LastModifierId = item.LastModifierId,
                            IsDeleted = item.IsDeleted,
                            DeleterId = item.DeleterId,
                            DeletionTime = item.DeletionTime
                        });
                    }
                    result = new PagedResultDto<OpenIddictApplicationDto>
                    {
                        Items = data,
                        TotalCount = await _applicationRepository.GetCountAsync()
                    };
                }
            }
            else
            {
                var data = new List<OpenIddictApplicationDto>();
                var list = await _applicationRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
                foreach (var item in list)
                {
                    var permissions = JsonSerializer.Deserialize<HashSet<string>>(item.Permissions);
                    var grantTypes = GetGrantTypeByPermission(permissions);
                    var scopes = GetScopeByPermission(permissions);
                    await AddApiScopeByPermission(permissions, scopes);
                    data.Add(new OpenIddictApplicationDto
                    {
                        Id = item.Id,
                        ClientId = item.ClientId,
                        ClientSecret = item.ClientSecret,
                        ClientUrl = item.ClientUri,
                        ConsentType = item.ClientUri,
                        DisplayName = item.DisplayName,
                        Type = item.Type,
                        GrantTypes = grantTypes,
                        Scopes = scopes,
                        RedirectUrls = JsonSerializer.Deserialize<List<string>>(item.RedirectUris),
                        PostLogoutRedirectUrls = JsonSerializer.Deserialize<List<string>>(item.PostLogoutRedirectUris),
                        CreationTime = item.CreationTime,
                        CreatorId = item.CreatorId,
                        LastModificationTime = item.LastModificationTime,
                        LastModifierId = item.LastModifierId,
                        IsDeleted = item.IsDeleted,
                        DeleterId = item.DeleterId,
                        DeletionTime = item.DeletionTime
                    });
                }
                result = new PagedResultDto<OpenIddictApplicationDto>
                {
                    Items = data,
                    TotalCount = await _applicationRepository.GetCountAsync()
                };
            }
            return result;
        }

        private async Task AddApiScopeByPermission(HashSet<string> permissions, List<string> scopes)
        {
            var apiScopes = await _scopeRepository.GetListAsync();
            var apiScopeList = apiScopes.Select(x => new
            {
                Name = "scp:" + x.Name,
                Value = x.Name
            });
            foreach (var apiScope in apiScopeList)
            {
                if (permissions.Contains(apiScope.Name))
                {
                    scopes.Add(apiScope.Value);
                }
            }
        }

        private static List<string> GetScopeByPermission(HashSet<string> permissions)
        {
            var scopes = new List<string>();
            if (permissions.Contains(OpenIddictConstants.Permissions.Scopes.Email))
            {
                scopes.Add(OpenIddictConstants.Scopes.Email);
            }
            if (permissions.Contains(OpenIddictConstants.Permissions.Scopes.Address))
            {
                scopes.Add(OpenIddictConstants.Scopes.Address);
            }
            if (permissions.Contains(OpenIddictConstants.Permissions.Scopes.Phone))
            {
                scopes.Add(OpenIddictConstants.Scopes.Phone);
            }
            if (permissions.Contains(OpenIddictConstants.Permissions.Scopes.Profile))
            {
                scopes.Add(OpenIddictConstants.Scopes.Profile);
            }
            if (permissions.Contains(OpenIddictConstants.Permissions.Scopes.Roles))
            {
                scopes.Add(OpenIddictConstants.Scopes.Roles);
            }
            return scopes;
        }

        private static List<string> GetGrantTypeByPermission(HashSet<string> permissions)
        {
            var grantTypes = new List<string>();
            if (permissions.Contains(OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode))
            {
                grantTypes.Add(OpenIddictConstants.GrantTypes.AuthorizationCode);
            }
            if (permissions.Contains(OpenIddictConstants.Permissions.GrantTypes.ClientCredentials))
            {
                grantTypes.Add(OpenIddictConstants.GrantTypes.ClientCredentials);
            }
            if (permissions.Contains(OpenIddictConstants.Permissions.GrantTypes.DeviceCode))
            {
                grantTypes.Add(OpenIddictConstants.GrantTypes.DeviceCode);
            }
            if (permissions.Contains(OpenIddictConstants.Permissions.GrantTypes.Implicit))
            {
                grantTypes.Add(OpenIddictConstants.GrantTypes.Implicit);
            }
            if (permissions.Contains(OpenIddictConstants.Permissions.GrantTypes.Password))
            {
                grantTypes.Add(OpenIddictConstants.GrantTypes.Password);
            }
            if (permissions.Contains(OpenIddictConstants.Permissions.GrantTypes.RefreshToken))
            {
                grantTypes.Add(OpenIddictConstants.GrantTypes.RefreshToken);
            }

            return grantTypes;
        }

        public async Task<PagedResultDto<OpenIddictScopeDto>> GetAllScopeAsync(PageListAndSortedRequestDto input)
        {
            var result = new PagedResultDto<OpenIddictScopeDto>();
            input.Sorting = !string.IsNullOrWhiteSpace(input.Sorting) ? input.Sorting : nameof(OpenIddictScopeDto.CreationTime);
            if (input.IsAll)
            {
                using (_dataFilter.Disable<ISoftDelete>())
                {
                    var list = await _scopeRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
                    result = new PagedResultDto<OpenIddictScopeDto>
                    {
                        Items = ObjectMapper.Map<List<OpenIddictScope>, List<OpenIddictScopeDto>>(list),
                        TotalCount = await _scopeRepository.GetCountAsync()
                    };
                };
            }
            else
            {
                var list = await _scopeRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
                result = new PagedResultDto<OpenIddictScopeDto>
                {
                    Items = ObjectMapper.Map<List<OpenIddictScope>, List<OpenIddictScopeDto>>(list),
                    TotalCount = await _scopeRepository.GetCountAsync()
                };
            }
            return result;
        }

        public async Task<OpenIddictApplicationDto> GetApplicationAsync(string id)
        {
            var entity = await _applicationRepository.FindAsync(Guid.Parse(id));
            if (entity != null)
            {
                var permissions = JsonSerializer.Deserialize<HashSet<string>>(entity.Permissions);
                var grantTypes = GetGrantTypeByPermission(permissions);
                var scopes = GetScopeByPermission(permissions);
                await AddApiScopeByPermission(permissions, scopes);
                var result = new OpenIddictApplicationDto
                {
                    Id = entity.Id,
                    ClientId = entity.ClientId,
                    ClientSecret = entity.ClientSecret,
                    ClientUrl = entity.ClientUri,
                    ConsentType = entity.ClientUri,
                    DisplayName = entity.DisplayName,
                    Type = entity.Type,
                    GrantTypes = grantTypes,
                    Scopes = scopes,
                    RedirectUrls = JsonSerializer.Deserialize<List<string>>(entity.RedirectUris),
                    PostLogoutRedirectUrls = JsonSerializer.Deserialize<List<string>>(entity.PostLogoutRedirectUris),
                    CreationTime = entity.CreationTime,
                    CreatorId = entity.CreatorId,
                    LastModificationTime = entity.LastModificationTime,
                    LastModifierId = entity.LastModifierId,
                    IsDeleted = entity.IsDeleted,
                    DeleterId = entity.DeleterId,
                    DeletionTime = entity.DeletionTime
                };
                return result;
            }
            return null;
        }

        public async Task<OpenIddictScopeDto> GetScopeAsync(string id)
        {
            var entity = await _scopeRepository.GetAsync(Guid.Parse(id));
            if (entity == null)
            {
                throw new UserFriendlyException("未找到对应的Scope", "500");
            }
            var result = ObjectMapper.Map<OpenIddictScope, OpenIddictScopeDto>(entity);
            return result;
        }

        public async Task<OpenIddictApplicationDto> UpdateApplicationAsync(string id, OpenIddictApplicationCreateUpdateDto input)
        {
            var entity = await _applicationRepository.FindAsync(Guid.Parse(id));
            if (entity != null)
            {
                await DeleteApplicationAsync(id);
                return await CreateApplicationAsync(input);
            }
            return null;
        }

        public async Task<OpenIddictScopeDto> UpdateScopeAsync(string id, OpenIddictScopeUpdateDto input)
        {
            var entity = await _scopeRepository.GetAsync(Guid.Parse(id));
            if (entity != null)
            {
                entity = ObjectMapper.Map(input, entity);
            }
            await _scopeRepository.UpdateAsync(entity);
            var result = ObjectMapper.Map<OpenIddictScope, OpenIddictScopeDto>(entity);
            return result;
        }

        public List<string> GetGrantTypes()
        {
            var type = typeof(OpenIddictConstants.GrantTypes);
            var result = new List<string>();
            foreach (var item in type.GetFields())
            {
                result.Add((string)item.GetRawConstantValue());
            }
            return result;
        }

        public List<string> GetClientTypes()
        {
            var type = typeof(OpenIddictConstants.ClientTypes);
            var result = new List<string>();
            foreach (var item in type.GetFields())
            {
                result.Add((string)item.GetRawConstantValue());
            }
            return result;
        }

        public List<string> GetConsentTypes()
        {
            var type = typeof(OpenIddictConstants.ConsentTypes);
            var result = new List<string>();
            foreach (var item in type.GetFields())
            {
                result.Add((string)item.GetRawConstantValue());
            }
            return result;
        }
    }
}
