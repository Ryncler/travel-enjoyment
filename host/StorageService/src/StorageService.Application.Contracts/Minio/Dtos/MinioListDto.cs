using System;
using System.Collections.Generic;
using System.Text;

namespace StorageService.Minio.Dtos
{
    public class MinioListDto
    {
        public string Prefix { get; set; }

        public string BucketName { get; set; }

        public bool IsRecursive { get; set; }
    }
}
