using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace StorageService.Minio
{
    public class AbpMinioOptions
    {
        public MinioConfiguration Minio { get; }

        public AbpMinioOptions()
        {
            Minio = new MinioConfiguration();
        }
    }
}
