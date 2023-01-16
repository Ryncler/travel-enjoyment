using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StorageService.Minio
{
    public class MinioProviderArgs
    {
        public string ObjectName { get; set; }

        [NotNull]
        public string BucketName { get; set; }

        public Stream FileStream { get; set; }

        public bool OverrideExisting { get; set; }
    }
}
