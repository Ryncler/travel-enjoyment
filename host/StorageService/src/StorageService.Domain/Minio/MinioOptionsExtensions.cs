using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

namespace StorageService.Minio
{
    public static class MinioOptionsExtensions
    {
        public static AbpMinioOptions TransientMinioProvider(this AbpMinioOptions options, ServiceConfigurationContext context)
        {
            context.Services.AddTransient(typeof(MinioNamingNormalizer));
            context.Services.AddTransient(typeof(IMinioProvider), typeof(MinioProvider));
            return options;
        }
    }
}
