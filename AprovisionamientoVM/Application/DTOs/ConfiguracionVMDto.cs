using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ConfiguracionVMDto
    {
        public string Proveedor { get; set; } = string.Empty;
        public int VCpus { get; set; }
        public int MemoriaGB { get; set; }
        public bool OptimizacionMemoria { get; set; }
        public bool OptimizacionDisco { get; set; }
        public string? ClaveSSH { get; set; }
    }
}
