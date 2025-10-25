using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Almacenamiento
    {

        public string Id { get; set; }
        public string Region { get; set; }
        public int? Iops { get; set; }
        public ProveedorNube Provider { get; set; }
        public int CapacidadGB { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Almacenamiento()
        {
            Id = Guid.NewGuid().ToString();
            FechaCreacion = DateTime.UtcNow;
            CapacidadGB = 100; // Valor por defecto
        }

        public Almacenamiento(string region, ProveedorNube provider)
        {
            Id = Guid.NewGuid().ToString();
            Region = region;
            Provider = provider;
            FechaCreacion = DateTime.UtcNow;
            CapacidadGB = 100;
        }
    }
}
