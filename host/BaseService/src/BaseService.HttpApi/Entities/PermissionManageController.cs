using BaseService.Permissions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;

namespace BaseService.Entities
{
    [Area(BaseServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = BaseServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/permission-manage")]
    public class PermissionManageController : BaseServiceController, IPermissionManageAppService
    {
        private readonly IPermissionManageAppService _permissionManageAppService;
        public PermissionManageController(IPermissionManageAppService permissionManageAppService)
        {
            _permissionManageAppService = permissionManageAppService;
        }

        [HttpGet]
        [Route("all-to-admin")]
        public Task AddAllPermissionToAdmin()
        {
            return _permissionManageAppService.AddAllPermissionToAdmin();
        }
    }
}
