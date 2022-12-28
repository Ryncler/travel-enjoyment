using System;
using System.Collections.Generic;
using System.Text;

namespace BaseService.OpenIddicts.Dtos
{
    public class OpenIddictApplicationDto
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Type { get; set; }

        public string ConsentType { get; set; }

        public string DisplayName { get; set; }

        public string ClientUrl { get; set; }

        public List<string> RedirectUrls { get; set; }

        public List<string> PostLogoutRedirectUrls { get; set; }

        public List<string> GrantTypes { get; set; }

        public List<string> Scopes { get; set; }
    }
}
