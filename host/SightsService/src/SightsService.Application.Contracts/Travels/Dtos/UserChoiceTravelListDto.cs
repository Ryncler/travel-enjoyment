using SightsService.TravelsManage.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace SightsService.TravelsManage.Dtos
{
    public class UserChoiceTravelListDto : FullAuditedEntityDto<Guid>
    {
        public List<TravelsDto> Travels { get; set; }
    }
}
