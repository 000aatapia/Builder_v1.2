using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Factories
{
    public class FactoriaRecursosAzure : IFactoriaRecursos
    {
        private static readonly HashSet<string> RegionesValidas = new()
    {
        "eastus", "eastus2", "westus", "westus2", "westus3",
        "centralus", "northeurope", "westeurope", "southeastasia"
    };

        public Red CrearRed(string region, List<string>? firewallRules, bool publicIP)
        {
            var red = new Red(region, ProveedorNube.Azure)
            {
                FirewallRules = firewallRules ?? new List<string>(),
                PublicIP = publicIP
            };

            return red;
        }

        public Almacenamiento CrearAlmacenamiento(string region, int? iops, int? capacidadGB)
        {
            var almacenamiento = new Almacenamiento(region, ProveedorNube.Azure)
            {
                Iops = iops ?? 5000,
                CapacidadGB = capacidadGB ?? 128
            };

            return almacenamiento;
        }

        public bool ValidarRegion(string region)
        {
            return RegionesValidas.Contains(region);
        }

        public string ObtenerNombreProveedor()
        {
            return "Azure";
        }
    }
}
