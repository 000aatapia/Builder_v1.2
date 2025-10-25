using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ConfiguracionInfraestructura
    {
        public string Id { get; set; }
        public MaquinaVirtual MaquinaVirtual { get; set; }
        public Red Red { get; set; }
        public Almacenamiento Almacenamiento { get; set; }
        public ProveedorNube Provider { get; set; }
        public TipoMaquina TipoMaquina { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; }

        public ConfiguracionInfraestructura()
        {
            Id = Guid.NewGuid().ToString();
            FechaCreacion = DateTime.UtcNow;
            Estado = "Activa";
        }

        public ConfiguracionInfraestructura(MaquinaVirtual vm, Red red, Almacenamiento almacenamiento)
        {
            Id = Guid.NewGuid().ToString();
            MaquinaVirtual = vm;
            Red = red;
            Almacenamiento = almacenamiento;
            Provider = vm.Provider;
            TipoMaquina = vm.TipoMaquina;
            FechaCreacion = DateTime.UtcNow;
            Estado = "Activa";
        }
    }
}
