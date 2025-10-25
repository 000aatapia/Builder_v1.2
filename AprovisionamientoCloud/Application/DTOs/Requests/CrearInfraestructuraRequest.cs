using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests
{
    public class CrearInfraestructuraRequest
    {
        public string Provider { get; set; } = string.Empty;
        public string TipoMaquina { get; set; } = string.Empty;
        public string TamañoInstancia { get; set; } = string.Empty;
        public RedDto Red { get; set; } = new();
        public AlmacenamientoDto Almacenamiento { get; set; } = new();
        public string? KeyPairName { get; set; }
        public List<string>? FirewallRulesAdicionales { get; set; }
    }
}
