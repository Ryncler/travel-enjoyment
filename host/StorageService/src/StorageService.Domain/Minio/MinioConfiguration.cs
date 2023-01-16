using System;
using System.Collections.Generic;
using System.Text;

namespace StorageService.Minio
{
    public class MinioConfiguration
    {
        public string EndPoint { get; set; }

        public string AccessKey { get; set; }

        public string SecretKey { get; set; }

        public string Region { get; set; }

        public bool WithSSL { get; set; }
    }
}
