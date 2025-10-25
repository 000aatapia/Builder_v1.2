using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests
{
    public class MaquinaVirtualDto
    {
        public string Provider { get; set; } = string.Empty;
        public int VCpus { get; set; }
        public int MemoryGB { get; set; }
        public bool MemoryOptimization { get; set; }
        public bool DiskOptimization { get; set; }
        public string? KeyPairName { get; set; }
    }
}
