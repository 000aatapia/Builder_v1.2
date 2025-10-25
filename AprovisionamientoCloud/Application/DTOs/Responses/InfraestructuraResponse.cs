using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses
{
    public class InfraestructuraResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
        public string TipoMaquina { get; set; } = string.Empty;
        public MaquinaVirtualResponse MaquinaVirtual { get; set; } = new();
        public RedResponse Red { get; set; } = new();
        public AlmacenamientoResponse Almacenamiento { get; set; } = new();
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
    public class MaquinaVirtualResponse
    {
        public string Id { get; set; } = string.Empty;
        public string NombreInstancia { get; set; } = string.Empty;
        public int VCpus { get; set; }
        public int MemoryGB { get; set; }
        public bool MemoryOptimization { get; set; }
        public bool DiskOptimization { get; set; }
        public string KeyPairName { get; set; } = string.Empty;
    }

    public class RedResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public List<string> FirewallRules { get; set; } = new();
        public bool PublicIP { get; set; }
    }

    public class AlmacenamientoResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public int? Iops { get; set; }
        public int CapacidadGB { get; set; }
    }
}
