using BaseService.User.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BaseService.User
{
    public interface IUserManageAppService : IApplicationService
    {
        Task<UserDto> GetAsync(string id);

        Task<PagedResultDto<UserDto>> GetAllAsync(PageListAndSortedRequestDto input);

        Task<UserDto> CreateAsync(UserCreateUpdateDto input);

        Task<UserDto> UpdateAsync(string id, UserCreateUpdateDto input);

        Task DeleteAsync(string id);

        Task UpdateUserPassWord(UserPasswordDto input);

        Task RegisterByEntry(RegisterUserByEntryDto input);

        Task<PagedResultDto<UserEntryInfoDto>> GetAllEntryInfo(PageListAndSortedRequestDto input);

        Task<bool> VerifyApply(VerifyApplyDto input);
    }
}
