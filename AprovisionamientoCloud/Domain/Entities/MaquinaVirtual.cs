using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MaquinaVirtual
    {
        public string Id { get; set; }
        public ProveedorNube Provider { get; set; }
        public int VCpus { get; set; }
        public int MemoryGB { get; set; }
        public bool MemoryOptimization { get; set; }
        public bool DiskOptimization { get; set; }
        public string? KeyPairName { get; set; }
        public TipoMaquina TipoMaquina { get; set; }
        public string? NombreInstancia { get; set; }
        public DateTime FechaCreacion { get; set; }

        public MaquinaVirtual()
        {
            Id = Guid.NewGuid().ToString();
            FechaCreacion = DateTime.UtcNow;
            KeyPairName = "default-key";
        }
    }
}
