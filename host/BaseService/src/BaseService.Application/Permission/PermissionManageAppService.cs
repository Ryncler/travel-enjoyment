using BaseService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Authorization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.PermissionManagement;

namespace BaseService.PermissionManage
{
    public class PermissionManageAppService : BaseServiceAppService, IPermissionManageAppService
    {
        private readonly IPermissionManager _permissionManager;

        public PermissionManageAppService(IPermissionManager permissionManager)
        {
            _permissionManager = permissionManager;
        }

        [Authorize]
        public async Task AddAllPermissionToAdminAsync()
        {
            if (!CurrentUser.IsInRole("admin"))
                throw new AbpAuthorizationException();

            var permissionList = await _permissionManager.GetAllAsync("R", "admin");
            permissionList.RemoveAll(x => x.Name.Equals("AbpIdentity.UserLookup"));
            foreach (var item in permissionList)
            {
                await _permissionManager.SetForRoleAsync("admin", item.Name, true);
            }
        }
    }
}
