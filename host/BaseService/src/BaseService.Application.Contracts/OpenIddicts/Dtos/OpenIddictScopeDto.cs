using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Volo.Abp.Application.Dtos;

namespace BaseService.OpenIddicts.Dtos
{
    public class OpenIddictScopeDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string Properties { get; set; }

        public string Resources { get; set; }
    }
}
