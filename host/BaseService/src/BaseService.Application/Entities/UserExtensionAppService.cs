using System;
using System.Linq;
using System.Threading.Tasks;
using BaseService.Permissions;
using BaseService.Entities.Dtos;
using Volo.Abp.Application.Services;

namespace BaseService.Entities;


public class UserExtensionAppService : AbstractKeyCrudAppService<UserExtension, UserExtensionDto, UserExtensionKey, PageListAndSortedRequestDto, UserExtensionCreateDto, UserExtensionUpdateDto>,
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
}
