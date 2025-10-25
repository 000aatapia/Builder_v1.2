using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Network : RecursoInfraestructura
    {

        public List<string>? FirewallRules { get; private set; }
        public bool? PublicIP { get; private set; }

        public Network()
        {
            FirewallRules = new List<string>();
        }

        public void EstablecerFirewallRules(List<string>? reglas)
        {
            FirewallRules = reglas ?? new List<string>();
        }

        public void AgregarReglaFirewall(string regla)
        {
            if (string.IsNullOrWhiteSpace(regla))
                throw new ArgumentException("La regla no puede estar vacía", nameof(regla));

            if (FirewallRules == null)
                FirewallRules = new List<string>();

            FirewallRules.Add(regla);
        }

        public void EstablecerPublicIP(bool? publicIp)
        {
            PublicIP = publicIp;
        }
    }
}
