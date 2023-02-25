using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ResourceManager.DTO.Configurations
{
    public class BlobStorageOptions
    {
        public string? Name { get; set; }
        public string? Account { get; set; }
        public string? Key { get; set; }
        public string? ContainerName { get; set; }
        public string? ConnectionString { get; set; }

    }
}
