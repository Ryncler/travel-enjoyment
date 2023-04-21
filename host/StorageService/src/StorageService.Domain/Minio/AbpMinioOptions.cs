using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace StorageService.Minio
{
    public class AbpMinioOptions
    {
        public string EndPoint { get; set; }

        public string AccessKey { get; set; }

        public string SecretKey { get; set; }

        public string Region { get; set; }

        public bool WithSSL { get; set; }

        public AbpMinioOptions()
        {
        }
    }
}
