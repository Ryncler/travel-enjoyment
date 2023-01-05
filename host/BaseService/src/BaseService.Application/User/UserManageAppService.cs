using BaseService.Entities;
using BaseService.User.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.Identity;

namespace BaseService.User
{
    public class UserManageAppService : BaseServiceAppService, IUserManageAppService
    {
        private readonly IUserExtensionRepository _userExtensionRepository;

        private readonly IIdentityUserAppService _identityUserAppService;

        public UserManageAppService(IUserExtensionRepository userExtensionRepository, IIdentityUserAppService identityUserAppService)
        {
            _userExtensionRepository = userExtensionRepository;
            _identityUserAppService = identityUserAppService;
        }
        public Task<UserDto> CreateAsync(UserCreateUpdateDto input)
        {
            _identityUserAppService.CreateAsync(new IdentityUserCreateDto
            {

            });
            throw new NotImplementedException();
        }

        public Task<List<UserDto>> GetAllAsync(PageListAndSortedRequestDto input)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> UpdateAsync(UserCreateUpdateDto input)
        {
            throw new NotImplementedException();
        }
    }
}
