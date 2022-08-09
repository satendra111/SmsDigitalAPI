using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommonEntity
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiryTime { get; set; }       
        public string EmailRegex { get; set; }
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
        public string FolderPath { get; set; }
        public string BlobUrl { get; set; }


    }
}
