using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SolicitudAprovisionamientoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string TipoMaquina { get; set; } = string.Empty;
        public string Proveedor { get; set; } = string.Empty;
        public string InstanceType { get; set; } = string.Empty; // Ej: "t3.medium", "D2s_v3", etc.
        public ConfiguracionRedDto Red { get; set; } = new();
        public ConfiguracionAlmacenamientoDto Almacenamiento { get; set; } = new();

        
        public string? ClaveSSH { get; set; }
        public bool OptimizacionMemoria { get; set; }
        public bool OptimizacionDisco { get; set; }
    }
}
