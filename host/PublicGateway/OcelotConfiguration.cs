﻿using Ocelot.Configuration.File;
using System.Collections.Generic;

namespace PublicGateway
{
    public class OcelotConfiguration
    {
        public string ServiceKey { get; set; }

        public string DownstreamScheme { get; set; }

        public List<FileHostAndPort> DownstreamHostAndPorts { get; set; }
    }


}
