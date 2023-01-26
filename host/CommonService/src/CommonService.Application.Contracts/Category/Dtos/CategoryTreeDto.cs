using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace CommonService.CategoryManage.Dtos
{
    public class CategoryTreeDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public List<CategoryTreeDto> Children { get; set; }
    }
}
