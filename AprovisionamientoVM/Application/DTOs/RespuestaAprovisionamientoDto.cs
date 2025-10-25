using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RespuestaAprovisionamientoDto
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public DetallesMaquinaVirtualDto? MaquinaVirtual { get; set; }
        public List<string> Errores { get; set; } = new();
    }
}
