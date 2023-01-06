using BaseService.Entities;
using BaseService.Entities.Dtos;
using BaseService.User.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.Uow;
using Volo.Abp.Users;
using static BaseService.Permissions.BaseServicePermissions;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace BaseService.User
{
    public class UserManageAppService : BaseServiceAppService, IUserManageAppService
    {
        private readonly IUserExtensionAppService _userExtensionAppService;

        private readonly IIdentityUserAppService _identityUserAppService;

        private readonly IIdentityRoleAppService _identityRoleAppService;

        private readonly IIdentityUserRepository _identityUserRepository;

        private readonly IDataFilter _dataFilter;

        public UserManageAppService(IUserExtensionAppService userExtensionAppService, IIdentityUserAppService identityUserAppService, IIdentityRoleAppService identityRoleAppService, IDataFilter dataFilter, IIdentityUserRepository identityUserRepository)
        {
            _userExtensionAppService = userExtensionAppService;
            _identityUserAppService = identityUserAppService;
            _identityRoleAppService = identityRoleAppService;
            _identityUserRepository = identityUserRepository;
            _dataFilter = dataFilter;
        }

        [UnitOfWork]
        public async Task<UserDto> CreateAsync(UserCreateUpdateDto input)
        {
            var user = await _identityUserAppService.CreateAsync(new IdentityUserCreateDto
            {
                UserName = input.UserName,
                Surname = input.UserName,
                Name = input.UserName,
                Email = input.Email,
                PhoneNumber = input.Phone,
                Password = input.Password,
                RoleNames = input.Roles.ToArray(),
                IsActive = input.Active
            });

            var userExtention = await _userExtensionAppService.CreateAsync(new UserExtensionCreateDto
            {
                UserId = user.Id,
                Sex = input.Sex,
                Avatar = input.Avatar,
                Profile = "",
                IsPushCommentMessage = true,
                IsPushPrivateMessage = true
            });

            return await Mapper(user, userExtention);
        }

        [UnitOfWork]
        public async Task DeleteAsync(string id)
        {
            await _identityUserAppService.DeleteAsync(Guid.Parse(id));
        }

        [UnitOfWork]
        public async Task<PagedResultDto<UserDto>> GetAllAsync(PageListAndSortedRequestDto input)
        {
            var result = new PagedResultDto<UserDto>();
            input.Sorting = !string.IsNullOrWhiteSpace(input.Sorting) ? input.Sorting : nameof(UserDto.CreationTime);
            if (input.IsAll)
            {
                using (_dataFilter.Disable<ISoftDelete>())
                {
                    var users = await _identityUserAppService.GetListAsync(new GetIdentityUsersInput
                    {
                        SkipCount = input.SkipCount,
                        Sorting = input.Sorting,
                        MaxResultCount = input.MaxResultCount
                    });
                    var userExtentions = await _userExtensionAppService.GetListAsync(input);

                    result = await MapperByList(users, userExtentions);
                }
            }
            else
            {
                var users = await _identityUserAppService.GetListAsync(new GetIdentityUsersInput
                {
                    SkipCount = input.SkipCount,
                    Sorting = input.Sorting,
                    MaxResultCount = input.MaxResultCount
                });
                var userExtentions = await _userExtensionAppService.GetListAsync(input);
                result = await MapperByList(users, userExtentions);
            }
            return result;
        }

        public async Task<UserDto> GetAsync(string id)
        {
            var user = await _identityUserAppService.GetAsync(Guid.Parse(id));
            var userExtension = await _userExtensionAppService.GetAsync(new UserExtensionKey
            {
                UserId = Guid.Parse(id)
            });
            if (user == null)
                throw new UserFriendlyException("未找到该用户", "500");

            return await Mapper(user, userExtension);
        }

        [UnitOfWork]
        public async Task<UserDto> UpdateAsync(string id, UserCreateUpdateDto input)
        {
            var user = await _identityUserAppService.UpdateAsync(Guid.Parse(id), new IdentityUserUpdateDto
            {
                UserName = input.UserName,
                Surname = input.UserName,
                Name = input.UserName,
                Email = input.Email,
                PhoneNumber = input.Phone,
                Password = input.Password,
                RoleNames = input.Roles.ToArray(),
                IsActive = input.Active
            });

            var userExtension = await _userExtensionAppService.UpdateAsync(new UserExtensionKey
            {
                UserId = Guid.Parse(id)
            },
              new UserExtensionUpdateDto
              {
                  Sex = input.Sex,
                  Avatar = input.Avatar,
              });

            return await Mapper(user, userExtension);
        }

        private async Task<UserDto> Mapper(IdentityUserDto user, UserExtensionDto userExtension)
        {
            var roles = await _identityUserAppService.GetRolesAsync(user.Id);
            return new UserDto
            {
                Id = user.Id,
                Sex = userExtension.Sex,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Active = user.IsActive,
                Delete = user.IsDeleted,
                Avatar = userExtension.Avatar,
                Roles = roles.Items.Select(x => x.Name).ToList(),
                CreationTime = user.CreationTime,
                CreatorId = user.CreatorId,
                LastModifierId = user.LastModifierId,
                LastModificationTime = user.LastModificationTime,
            };
        }

        private async Task<PagedResultDto<UserDto>> MapperByList(PagedResultDto<IdentityUserDto> users, PagedResultDto<UserExtensionDto> userExtentions)
        {
            var result = new List<UserDto>();
            foreach (var item in users.Items)
            {
                var roles = await _identityUserAppService.GetRolesAsync(item.Id);
                var userExtension = userExtentions.Items.Where(x => x.UserId.Equals(item.Id)).FirstOrDefault();
                if (userExtension != null)
                {
                    result.Add(new UserDto
                    {
                        Id = item.Id,
                        Sex = userExtension.Sex,
                        UserName = item.UserName,
                        Email = item.Email,
                        Phone = item.PhoneNumber,
                        Active = item.IsActive,
                        Delete = item.IsDeleted,
                        Avatar = userExtension.Avatar,
                        Roles = roles.Items.Select(x => x.Name).ToList(),
                        CreationTime = item.CreationTime,
                        CreatorId = item.CreatorId,
                        LastModifierId = item.LastModifierId,
                        LastModificationTime = item.LastModificationTime,
                    });
                }
            }

            return new PagedResultDto<UserDto>
            {
                Items = result,
                TotalCount = await _identityUserRepository.GetCountAsync()
            };
        }
    }
}
