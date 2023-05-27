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

        Task<string> GetNameByIdAsync(string id);

        Task<PagedResultDto<UserDto>> GetAllAsync(PageListAndSortedRequestDto input);

        Task<UserDto> CreateAsync(UserCreateUpdateDto input);

        Task<UserDto> UpdateAsync(string id, UserCreateUpdateDto input);

        Task DeleteAsync(string id);

        Task UpdateUserPassWordAsync(UserPasswordDto input);

        Task RegisterByEntryAsync(RegisterUserDto input);

        Task RegisterByUserAsync(RegisterUserDto input);

        Task<PagedResultDto<UserEntryInfoDto>> GetAllEntryInfoAsync(PageListAndSortedRequestDto input);

        Task<bool> VerifyApplyAsync(VerifyApplyDto input);
    }
}
