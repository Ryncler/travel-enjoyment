using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BaseService.Enums
{
    public enum ApplyStatus
    {
        [Description("申请中")]
        Applying = 0,

        [Description("成功")]
        Success = 1,

        [Description("驳回")]
        Failed = 2,
    }
}
