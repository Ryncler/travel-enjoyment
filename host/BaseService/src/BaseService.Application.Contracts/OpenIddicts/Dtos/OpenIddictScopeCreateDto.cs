using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace BaseService.OpenIddicts.Dtos
{
    public class OpenIddictScopeCreateDto
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string Properties { get; set; }

        public string Resources { get; set; }
    }
}
