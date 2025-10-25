using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DetallesMaquinaVirtualDto
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Proveedor { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }

        // Configuración de cómputo
        public int VCpus { get; set; }
        public int MemoriaGB { get; set; }
        public bool OptimizacionMemoria { get; set; }
        public bool OptimizacionDisco { get; set; }
        public string? ClaveSSH { get; set; }

        // Recursos asociados
        public DetalleRedDto Red { get; set; } = new();
        public DetalleAlmacenamientoDto Almacenamiento { get; set; } = new();

        public string Resumen { get; set; } = string.Empty;
    }
}
