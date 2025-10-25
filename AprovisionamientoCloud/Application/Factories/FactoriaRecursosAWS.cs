using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Factories
{
    public class FactoriaRecursosAWS : IFactoriaRecursos
    {
        private static readonly HashSet<string> RegionesValidas = new()
    {
        "us-east-1", "us-east-2", "us-west-1", "us-west-2",
        "eu-west-1", "eu-central-1", "ap-southeast-1", "ap-northeast-1"
    };

        public Red CrearRed(string region, List<string>? firewallRules, bool publicIP)
        {
            var red = new Red(region, ProveedorNube.AWS)
            {
                FirewallRules = firewallRules ?? new List<string>(),
                PublicIP = publicIP
            };

            return red;
        }

        public Almacenamiento CrearAlmacenamiento(string region, int? iops, int? capacidadGB)
        {
            var almacenamiento = new Almacenamiento(region, ProveedorNube.AWS)
            {
                Iops = iops ?? 3000,
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
            return "AWS";
        }
    }
}
