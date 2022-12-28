using System;
using System.Collections.Generic;
using System.Text;

namespace BaseService.OpenIddicts.Dtos
{
    public class OpenIddictScopeUpdateDto
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string Properties { get; set; }

        public string Resources { get; set; }
    }
}
