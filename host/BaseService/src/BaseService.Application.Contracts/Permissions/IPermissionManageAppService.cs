using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BaseService.Permissions
{
    public interface IPermissionManageAppService
    {
        Task AddAllPermissionToAdmin();
    }
}
