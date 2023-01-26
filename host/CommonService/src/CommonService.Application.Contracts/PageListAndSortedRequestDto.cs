using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace CommonService
{
    public class PageListAndSortedRequestDto : PagedAndSortedResultRequestDto
    {
        public override int MaxResultCount
        {
            get => base.MaxResultCount;
            set => base.MaxResultCount = value;
        }

        public override int SkipCount
        {
            get => base.SkipCount;
            set => base.SkipCount = (value - 1) * MaxResultCount;
        }

        /// <summary>
        /// 是否显示全部
        /// </summary>
        public bool IsAll { get; set; } = false;
    }
}
