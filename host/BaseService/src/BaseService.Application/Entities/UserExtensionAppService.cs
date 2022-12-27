using System;
using System.Linq;
using System.Threading.Tasks;
using BaseService.Permissions;
using BaseService.Entities.Dtos;
using Volo.Abp.Application.Services;

namespace BaseService.Entities;


public class UserExtensionAppService : AbstractKeyCrudAppService<UserExtension, UserExtensionDto, UserExtensionKey, UserExtensionGetListInput, UserExtensionCreateDto, UserExtensionUpdateDto>,
    IUserExtensionAppService
{
    private readonly IUserExtensionRepository _repository;

    public UserExtensionAppService(IUserExtensionRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override Task DeleteByIdAsync(UserExtensionKey id)
    {
        // TODO: AbpHelper generated
        return _repository.DeleteAsync(e =>
            e.UserId == id.UserId
        );
    }

    protected override async Task<UserExtension> GetEntityByIdAsync(UserExtensionKey id)
    {
        // TODO: AbpHelper generated
        return await AsyncExecuter.FirstOrDefaultAsync(
            (await _repository.WithDetailsAsync()).Where(e =>
                e.UserId == id.UserId
            ));
    }

    protected override IQueryable<UserExtension> ApplyDefaultSorting(IQueryable<UserExtension> query)
    {
        // TODO: AbpHelper generated
        return query.OrderBy(e => e.UserId);
    }

    protected override async Task<IQueryable<UserExtension>> CreateFilteredQueryAsync(UserExtensionGetListInput input)
    {
        // TODO: AbpHelper generated
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(input.UserId != null, x => x.UserId == input.UserId)
            .WhereIf(!input.Avatar.IsNullOrWhiteSpace(), x => x.Avatar.Contains(input.Avatar))
            .WhereIf(!input.Profile.IsNullOrWhiteSpace(), x => x.Profile.Contains(input.Profile))
            .WhereIf(input.IsPushPrivateMessage != null, x => x.IsPushPrivateMessage == input.IsPushPrivateMessage)
            .WhereIf(input.IsPushCommentMessage != null, x => x.IsPushCommentMessage == input.IsPushCommentMessage)
            ;
    }
}
