using BaseService.Email;
using BaseService.Entities;
using BaseService.Entities.Dtos;
using BaseService.EntryInfos;
using BaseService.Enums;
using BaseService.Permissions;
using BaseService.User.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Account.Settings;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.Settings;
using Volo.Abp.Uow;

namespace BaseService.User
{
    public class UserManageAppService : BaseServiceAppService, IUserManageAppService
    {
        private readonly IUserExtensionAppService _userExtensionAppService;

        private readonly IIdentityUserAppService _identityUserAppService;

        private readonly IIdentityRoleAppService _identityRoleAppService;

        private readonly IIdentityUserRepository _identityUserRepository;

        private readonly IDataFilter _dataFilter;

        private readonly IdentityUserManager _identityUserManager;

        private readonly IOptions<IdentityOptions> _identityOptions;

        private readonly IEntryInfoManageAppService _entryInfoManageAppService;

        private readonly IConfiguration _configuration;

        private readonly IEmailAppService _emailAppService;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserManageAppService(IUserExtensionAppService userExtensionAppService, IIdentityUserAppService identityUserAppService, IIdentityRoleAppService identityRoleAppService,
            IDataFilter dataFilter, IIdentityUserRepository identityUserRepository, IdentityUserManager identityUserManager, IOptions<IdentityOptions> identityOptions,
            IEntryInfoManageAppService entryInfoManageAppService, IConfiguration configuration, IEmailAppService emailAppService, IHttpContextAccessor httpContextAccessor)
        {
            _userExtensionAppService = userExtensionAppService;
            _identityUserAppService = identityUserAppService;
            _identityRoleAppService = identityRoleAppService;
            _identityUserRepository = identityUserRepository;
            _dataFilter = dataFilter;
            _identityUserManager = identityUserManager;
            _identityOptions = identityOptions;
            _entryInfoManageAppService = entryInfoManageAppService;
            _configuration = configuration;
            _emailAppService = emailAppService;
            _httpContextAccessor = httpContextAccessor;
        }

        [UnitOfWork]
        [Authorize(BaseServicePermissions.UserManage.Create)]
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
                Profile = input.Profile,
                IsPushCommentMessage = true,
                IsPushPrivateMessage = true
            });

            return await MapperAsync(user, userExtention);
        }

        [UnitOfWork]
        [Authorize(BaseServicePermissions.UserManage.Delete)]
        public async Task DeleteAsync(string id)
        {
            await _identityUserAppService.DeleteAsync(Guid.Parse(id));
        }

        [UnitOfWork]
        [Authorize(BaseServicePermissions.UserManage.Default)]
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

                    result = await MapperByListAsync(users, userExtentions);
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
                result = await MapperByListAsync(users, userExtentions);
            }
            return result;
        }

        [Authorize(BaseServicePermissions.UserManage.Default)]
        public async Task<UserDto> GetAsync(string id)
        {
            var user = await _identityUserAppService.GetAsync(Guid.Parse(id));
            var userExtension = await _userExtensionAppService.GetAsync(new UserExtensionKey
            {
                UserId = Guid.Parse(id)
            });
            if (user == null)
                throw new UserFriendlyException("未找到该用户", "500");
            if (userExtension == null)
            {
                var roles = await _identityUserAppService.GetRolesAsync(user.Id);
                return new UserDto
                {
                    Id = user.Id,
                    Sex = false,
                    UserName = user.UserName,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    Active = user.IsActive,
                    Delete = user.IsDeleted,
                    Profile = "",
                    Avatar = "",
                    Roles = roles.Items.Select(x => x.Name).ToList(),
                    CreationTime = user.CreationTime,
                    CreatorId = user.CreatorId,
                    LastModifierId = user.LastModifierId,
                    LastModificationTime = user.LastModificationTime,
                };
            }
            return await MapperAsync(user, userExtension);
        }

        [UnitOfWork]
        public async Task RegisterByEntryAsync(RegisterUserDto input)
        {
            await CheckSelfRegistrationAsync();
            await _identityOptions.SetAsync();
            var user = new IdentityUser(GuidGenerator.Create(), input.UserName, input.Email, CurrentTenant.Id);
            user.SetIsActive(false);
            user.SetPhoneNumber(input.Phone, false);

            await _entryInfoManageAppService.CreateAsync(new EntryInfoCreateUpdateDto
            {
                UserId = user.Id,
                CompanyName = input.CompanyName,
                UnifiedCreditCode = input.UnifiedCreditCode,
                Status = Enums.ApplyStatus.Applying,
                ApplyTiem = DateTime.Now,
            });

            (await _identityUserManager.CreateAsync(user, input.Password)).CheckErrors();
            await _identityUserManager.SetEmailAsync(user, input.Email);
            await _identityUserManager.AddToRoleAsync(user, _configuration["RegisterRole:EntryRole"]);
            await _identityUserManager.SetLockoutEnabledAsync(user, true);
            return;
        }

        [UnitOfWork]
        public async Task RegisterByUserAsync(RegisterUserDto input)
        {
            await CheckSelfRegistrationAsync();
            await _identityOptions.SetAsync();
            var user = new IdentityUser(GuidGenerator.Create(), input.UserName, input.Email, CurrentTenant.Id);
            user.SetIsActive(false);
            user.SetPhoneNumber(input.Phone, false);
            (await _identityUserManager.CreateAsync(user, input.Password)).CheckErrors();
            await _identityUserManager.SetEmailAsync(user, input.Email);
            await _identityUserManager.AddToRoleAsync(user, _configuration["RegisterRole:UserRole"]);
            await _identityUserManager.SetLockoutEnabledAsync(user, true);

            using (var sr = new StreamReader(@"./Html/active.html", Encoding.UTF8))
            {
                string strhtml = sr.ReadToEnd();
                var currentHost = string.Format("{0}://{1}/verify?id={2}", _httpContextAccessor.HttpContext.Request.Scheme, _httpContextAccessor.HttpContext.Request.Host, user.Id);
                var str = strhtml.Replace("{url}", currentHost);

                await _emailAppService.SendEmailAsync(new Email.Dtos.SendDto
                {
                    Email = user.Email,
                    Subject = "激活您的账户",
                    Data = str
                });
            }
        }

        [UnitOfWork]
        [Authorize(BaseServicePermissions.UserManage.Update)]
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
                  Profile = input.Profile,
              });

            return await MapperAsync(user, userExtension);
        }

        [Authorize]
        [UnitOfWork]
        public async Task UpdateUserPassWordAsync(UserPasswordDto input)
        {
            var user = await _identityUserManager.GetByIdAsync(Guid.Parse(input.UserId));
            if (user == null) throw new UserFriendlyException("未找到该用户", "500");
            var isOldPassword = await _identityUserManager.CheckPasswordAsync(user, input.OldPassword);
            if (!isOldPassword) throw new UserFriendlyException("旧密码错误", "500");
            if (string.IsNullOrWhiteSpace(input.NewPassword)) throw new UserFriendlyException("新密码不能为空", "500");

            await _identityOptions.SetAsync();
            await _identityUserManager.RemovePasswordAsync(user);
            await _identityUserManager.AddPasswordAsync(user, input.NewPassword);
        }

        private async Task<UserDto> MapperAsync(IdentityUserDto user, UserExtensionDto userExtension)
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
                Profile = userExtension.Profile,
                Avatar = userExtension.Avatar,
                Roles = roles.Items.Select(x => x.Name).ToList(),
                CreationTime = user.CreationTime,
                CreatorId = user.CreatorId,
                LastModifierId = user.LastModifierId,
                LastModificationTime = user.LastModificationTime,
            };
        }

        private async Task<PagedResultDto<UserDto>> MapperByListAsync(PagedResultDto<IdentityUserDto> users, PagedResultDto<UserExtensionDto> userExtentions)
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
                        Profile = userExtension.Profile,
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

        protected virtual async Task CheckSelfRegistrationAsync()
        {
            if (!await SettingProvider.IsTrueAsync(AccountSettingNames.IsSelfRegistrationEnabled))
            {
                throw new UserFriendlyException(L["SelfRegistrationDisabledMessage"]);
            }
        }

        [UnitOfWork]
        [Authorize(BaseServicePermissions.UserManage.Default)]
        public async Task<PagedResultDto<UserEntryInfoDto>> GetAllEntryInfoAsync(PageListAndSortedRequestDto input)
        {
            var result = new PagedResultDto<UserEntryInfoDto>();
            input.Sorting = !string.IsNullOrWhiteSpace(input.Sorting) ? input.Sorting : nameof(UserEntryInfoDto.CreationTime);
            if (input.IsAll)
            {
                using (_dataFilter.Disable<ISoftDelete>())
                {
                    var data = new List<UserEntryInfoDto>();
                    var entryInfos = await _entryInfoManageAppService.GetListAsync(input);
                    foreach (var item in entryInfos.Items)
                    {
                        var user = await _identityUserAppService.GetAsync(item.UserId);
                        data.Add(new UserEntryInfoDto
                        {
                            Id = item.Id,
                            UserName = user.UserName,
                            Email = user.Email,
                            Phone = user.PhoneNumber,
                            Active = user.IsActive,
                            CompanyName = item.CompanyName,
                            UnifiedCreditCode = item.UnifiedCreditCode,
                            Status = item.Status,
                            ApplyTiem = item.ApplyTiem,
                            FailedDescription = item.FailedDescription,
                            CreationTime = item.CreationTime,
                            CreatorId = item.CreatorId,
                            LastModificationTime = item.LastModificationTime,
                            LastModifierId = item.LastModifierId,
                            IsDeleted = item.IsDeleted,
                            DeleterId = item.DeleterId,
                            DeletionTime = item.DeletionTime
                        });
                    }
                    result = new PagedResultDto<UserEntryInfoDto>
                    {
                        Items = data,
                        TotalCount = entryInfos.TotalCount
                    };
                }
            }
            else
            {
                var data = new List<UserEntryInfoDto>();
                var entryInfos = await _entryInfoManageAppService.GetListAsync(input);
                foreach (var item in entryInfos.Items)
                {
                    var user = await _identityUserAppService.GetAsync(item.UserId);
                    data.Add(new UserEntryInfoDto
                    {
                        Id = item.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        Phone = user.PhoneNumber,
                        Active = user.IsActive,
                        CompanyName = item.CompanyName,
                        UnifiedCreditCode = item.UnifiedCreditCode,
                        Status = item.Status,
                        ApplyTiem = item.ApplyTiem,
                        FailedDescription = item.FailedDescription,
                        CreationTime = item.CreationTime,
                        CreatorId = item.CreatorId,
                        LastModificationTime = item.LastModificationTime,
                        LastModifierId = item.LastModifierId,
                        IsDeleted = item.IsDeleted,
                        DeleterId = item.DeleterId,
                        DeletionTime = item.DeletionTime
                    });
                }
                result = new PagedResultDto<UserEntryInfoDto>
                {
                    Items = data,
                    TotalCount = entryInfos.TotalCount
                };
            }
            return result;
        }

        [UnitOfWork]
        public async Task<bool> VerifyApplyAsync(VerifyApplyDto input)
        {
            var entryId = Guid.Parse(input.Id);
            var entryInfo = await _entryInfoManageAppService.GetAsync(entryId);
            var user = await _identityUserManager.GetByIdAsync(entryInfo.UserId);
            if (entryInfo == null)
                throw new UserFriendlyException("未找到该申请", "500");
            if (user == null)
                throw new UserFriendlyException("未找到该用户", "500");
            if (input.IsSuccess)
            {
                await _entryInfoManageAppService.UpdateAsync(entryId, new EntryInfoCreateUpdateDto
                {
                    Status = ApplyStatus.Success,
                    ApplyTiem = entryInfo.ApplyTiem,
                    CompanyName = entryInfo.CompanyName,
                    UserId = entryInfo.UserId,
                    UnifiedCreditCode = entryInfo.UnifiedCreditCode,
                });

                await _userExtensionAppService.CreateAsync(new UserExtensionCreateDto
                {
                    UserId = entryInfo.UserId,
                    Sex = true,
                    Avatar = "",
                    Profile = "",
                    IsPushCommentMessage = false,
                    IsPushPrivateMessage = false
                });

                user.SetIsActive(true);
                await _identityUserManager.SetLockoutEnabledAsync(user, false);

                using (var sr = new StreamReader(@"./Html/message.html", Encoding.UTF8))
                {
                    string str = sr.ReadToEnd();
                    await _emailAppService.SendEmailAsync(new Email.Dtos.SendDto
                    {
                        Email = user.Email,
                        Subject = "您的账户已激活",
                        Data = str
                    });
                }
                return true;
            }
            else
            {
                await _entryInfoManageAppService.UpdateAsync(entryId, new EntryInfoCreateUpdateDto
                {
                    Status = ApplyStatus.Failed,
                    FailedDescription = input.Description,
                    ApplyTiem = entryInfo.ApplyTiem,
                    CompanyName = entryInfo.CompanyName,
                    UserId = entryInfo.UserId,
                    UnifiedCreditCode = entryInfo.UnifiedCreditCode,
                });
                return false;
            }
        }
    }
}
