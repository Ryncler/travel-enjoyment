using SightsService.TravelsManage.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace SightsService.TravelsManage.Dtos
{
    [Serializable]
    public class UserChoiceTravelListDto : FullAuditedEntityDto<Guid>
    {
        public Guid OneTravelsId { get; set; }

        public Guid TwoTravelsId { get; set; }

        public List<TravelsDto> Travels { get; set; } = new List<TravelsDto>();
    }
}
