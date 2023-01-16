using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StorageService.Minio.Dtos
{
    public class MinioDto
    {
        public string ObjectName { get; set; }

        public string BucketName { get; set; }

        public Stream FileStream { get; set; }

        public bool OverrideExisting { get; set; }
    }
}
