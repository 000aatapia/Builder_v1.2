using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Factories
{
    public class FactoriaRecursosGCP : IFactoriaRecursos
    {
        private static readonly HashSet<string> RegionesValidas = new()
    {
        "us-central1", "us-east1", "us-west1", "us-west2",
        "europe-west1", "europe-west2", "asia-southeast1", "asia-northeast1"
    };

        public Red CrearRed(string region, List<string>? firewallRules, bool publicIP)
        {
            var red = new Red(region, ProveedorNube.GCP)
            {
                FirewallRules = firewallRules ?? new List<string>(),
                PublicIP = publicIP
            };

            return red;
        }

        public Almacenamiento CrearAlmacenamiento(string region, int? iops, int? capacidadGB)
        {
            var almacenamiento = new Almacenamiento(region, ProveedorNube.GCP)
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
            return "GCP";
        }
    }
}
