using System;
using System.Collections.Generic;
using System.Text;

namespace StorageService.Minio
{
    public class MinioProviderResult
    {
        public bool Result { get; set; }

        public string ObjectName { get; set; }

        public string ErrorObjecName { get; set; }
    }
}
