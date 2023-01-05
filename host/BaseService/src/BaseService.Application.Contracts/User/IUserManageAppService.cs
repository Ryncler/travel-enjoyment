using BaseService.User.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace BaseService.User
{
    public interface IUserManageAppService : IApplicationService
    {
        Task<UserDto> GetAsync(string id);

        Task<List<UserDto>> GetAllAsync(PageListAndSortedRequestDto input);

        Task<UserDto> CreateAsync(UserCreateUpdateDto input);

        Task<UserDto> UpdateAsync(UserCreateUpdateDto input);

    }
}
