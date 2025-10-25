using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class ConfiguracionRed
    {
        public string Region { get; private set; }
        public List<string>? ReglasFirewall { get; private set; }
        public bool IPPublica { get; private set; }

        public ConfiguracionRed(
            string region,
            List<string>? reglasFirewall = null,
            bool ipPublica = false)
        {
            if (string.IsNullOrWhiteSpace(region))
                throw new ValidacionDominioException("La región es obligatoria");

            Region = region;
            ReglasFirewall = reglasFirewall ?? new List<string>();
            IPPublica = ipPublica;
        }
    }
}
