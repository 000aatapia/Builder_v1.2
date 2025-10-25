using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class ConfiguracionAlmacenamiento
    {
        public string Region { get; private set; }
        public int? IOPS { get; private set; }

        public ConfiguracionAlmacenamiento(
            string region,
            int? iops = null)
        {
            if (string.IsNullOrWhiteSpace(region))
                throw new ValidacionDominioException("La región es obligatoria");

            if (iops.HasValue && iops.Value < 0)
                throw new ValidacionDominioException("El IOPS no puede ser negativo");

            Region = region;
            IOPS = iops ?? 3000;
        }
    }
}