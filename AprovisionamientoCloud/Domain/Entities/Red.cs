using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Red
    {
        public string Id { get; set; }
        public string Region { get; set; }
        public List<string>? FirewallRules { get; set; }
        public bool PublicIP { get; set; }
        public ProveedorNube Provider { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Red()
        {
            Id = Guid.NewGuid().ToString();
            FechaCreacion = DateTime.UtcNow;
            FirewallRules = new List<string>();
            PublicIP = false;
        }

        public Red(string region, ProveedorNube provider)
        {
            Id = Guid.NewGuid().ToString();
            Region = region;
            Provider = provider;
            FechaCreacion = DateTime.UtcNow;
            FirewallRules = new List<string>();
            PublicIP = false;
        }
    }
}
