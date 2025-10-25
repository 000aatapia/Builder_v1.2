using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class ConfiguracionVM
    {
        public ProveedorNube Proveedor { get; private set; }
        public int VCpus { get; private set; }
        public int MemoriaGB { get; private set; }
        public bool OptimizacionMemoria { get; private set; }
        public bool OptimizacionDisco { get; private set; }
        public string? ClaveSSH { get; private set; }

        public ConfiguracionVM(
            ProveedorNube proveedor,
            int vCpus,
            int memoriaGB,
            bool optimizacionMemoria = false,
            bool optimizacionDisco = false,
            string? claveSSH = null)
        {
            if (vCpus <= 0)
                throw new ValidacionDominioException("El número de vCPUs debe ser mayor a 0");

            if (memoriaGB <= 0)
                throw new ValidacionDominioException("La memoria debe ser mayor a 0 GB");

            Proveedor = proveedor;
            VCpus = vCpus;
            MemoriaGB = memoriaGB;
            OptimizacionMemoria = optimizacionMemoria;
            OptimizacionDisco = optimizacionDisco;
            ClaveSSH = claveSSH ?? "default-key";
        }
    }
}
