using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Factories
{
    public class FactoriaRecursosOnPremise : IFactoriaRecursos
    {
        private static readonly HashSet<string> RegionesValidas = new()
    {
        "datacenter-1", "datacenter-2", "datacenter-3",
        "local-site-a", "local-site-b"
    };

        public Red CrearRed(string region, List<string>? firewallRules, bool publicIP)
        {
            var red = new Red(region, ProveedorNube.OnPremise)
            {
                FirewallRules = firewallRules ?? new List<string>(),
                PublicIP = publicIP
            };

            return red;
        }

        public Almacenamiento CrearAlmacenamiento(string region, int? iops, int? capacidadGB)
        {
            var almacenamiento = new Almacenamiento(region, ProveedorNube.OnPremise)
            {
                Iops = iops ?? 2000,
                CapacidadGB = capacidadGB ?? 100
            };

            return almacenamiento;
        }

        public bool ValidarRegion(string region)
        {
            return RegionesValidas.Contains(region);
        }

        public string ObtenerNombreProveedor()
        {
            return "OnPremise";
        }
    }
}
