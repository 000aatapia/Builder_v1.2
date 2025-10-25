using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Directors
{

    public class EspecificacionInstancia
    {
        public int VCpus { get; set; }
        public int MemoriaGB { get; set; }
    }

    public static class ConfiguracionesTipoMaquina
    {
        // AWS - Configuraciones
        private static readonly Dictionary<string, EspecificacionInstancia> _configuracionesAWS = new()
    {
        // General Purpose (Standard)
        { "t3.medium", new EspecificacionInstancia { VCpus = 2, MemoriaGB = 4 } },
        { "m5.large", new EspecificacionInstancia { VCpus = 2, MemoriaGB = 8 } },
        { "m5.xlarge", new EspecificacionInstancia { VCpus = 4, MemoriaGB = 16 } },
        
        // Memory-Optimized
        { "r5.large", new EspecificacionInstancia { VCpus = 2, MemoriaGB = 16 } },
        { "r5.xlarge", new EspecificacionInstancia { VCpus = 4, MemoriaGB = 32 } },
        { "r5.2xlarge", new EspecificacionInstancia { VCpus = 8, MemoriaGB = 64 } },
        
        // Compute-Optimized
        { "c5.large", new EspecificacionInstancia { VCpus = 2, MemoriaGB = 4 } },
        { "c5.xlarge", new EspecificacionInstancia { VCpus = 4, MemoriaGB = 8 } },
        { "c5.2xlarge", new EspecificacionInstancia { VCpus = 8, MemoriaGB = 16 } }
    };

        private static readonly Dictionary<string, EspecificacionInstancia> _configuracionesAzure = new()
    {

        { "D2s_v3", new EspecificacionInstancia { VCpus = 2, MemoriaGB = 8 } },
        { "D4s_v3", new EspecificacionInstancia { VCpus = 4, MemoriaGB = 16 } },
        { "D8s_v3", new EspecificacionInstancia { VCpus = 8, MemoriaGB = 32 } },
        

        { "E2s_v3", new EspecificacionInstancia { VCpus = 2, MemoriaGB = 16 } },
        { "E4s_v3", new EspecificacionInstancia { VCpus = 4, MemoriaGB = 32 } },
        { "E8s_v3", new EspecificacionInstancia { VCpus = 8, MemoriaGB = 64 } },
        

        { "F2s_v2", new EspecificacionInstancia { VCpus = 2, MemoriaGB = 4 } },
        { "F4s_v2", new EspecificacionInstancia { VCpus = 4, MemoriaGB = 8 } },
        { "F8s_v2", new EspecificacionInstancia { VCpus = 8, MemoriaGB = 16 } }
    };

 
        private static readonly Dictionary<string, EspecificacionInstancia> _configuracionesGCP = new()
    {
 
        { "e2-standard-2", new EspecificacionInstancia { VCpus = 2, MemoriaGB = 8 } },
        { "e2-standard-4", new EspecificacionInstancia { VCpus = 4, MemoriaGB = 16 } },
        { "e2-standard-8", new EspecificacionInstancia { VCpus = 8, MemoriaGB = 32 } },
        

        { "n2-highmem-2", new EspecificacionInstancia { VCpus = 2, MemoriaGB = 16 } },
        { "n2-highmem-4", new EspecificacionInstancia { VCpus = 4, MemoriaGB = 32 } },
        { "n2-highmem-8", new EspecificacionInstancia { VCpus = 8, MemoriaGB = 64 } },
        

        { "n2-highcpu-2", new EspecificacionInstancia { VCpus = 2, MemoriaGB = 2 } },
        { "n2-highcpu-4", new EspecificacionInstancia { VCpus = 4, MemoriaGB = 4 } },
        { "n2-highcpu-8", new EspecificacionInstancia { VCpus = 8, MemoriaGB = 8 } }
    };

        private static readonly Dictionary<string, EspecificacionInstancia> _configuracionesOnPremise = new()
    {

        { "onprem-std1", new EspecificacionInstancia { VCpus = 2, MemoriaGB = 4 } },
        { "onprem-std2", new EspecificacionInstancia { VCpus = 4, MemoriaGB = 8 } },
        { "onprem-std3", new EspecificacionInstancia { VCpus = 8, MemoriaGB = 16 } },
        

        { "onprem-mem1", new EspecificacionInstancia { VCpus = 2, MemoriaGB = 16 } },
        { "onprem-mem2", new EspecificacionInstancia { VCpus = 4, MemoriaGB = 32 } },
        { "onprem-mem3", new EspecificacionInstancia { VCpus = 8, MemoriaGB = 64 } },
        
 
        { "onprem-cpu1", new EspecificacionInstancia { VCpus = 2, MemoriaGB = 2 } },
        { "onprem-cpu2", new EspecificacionInstancia { VCpus = 4, MemoriaGB = 4 } },
        { "onprem-cpu3", new EspecificacionInstancia { VCpus = 8, MemoriaGB = 8 } }
    };

        public static EspecificacionInstancia? ObtenerEspecificacion(ProveedorNube proveedor, string instanceType)
        {
            var configuraciones = proveedor switch
            {
                ProveedorNube.AWS => _configuracionesAWS,
                ProveedorNube.Azure => _configuracionesAzure,
                ProveedorNube.GCP => _configuracionesGCP,
                ProveedorNube.OnPremise => _configuracionesOnPremise,
                _ => null
            };

            if (configuraciones != null && configuraciones.TryGetValue(instanceType, out var especificacion))
            {
                return especificacion;
            }

            return null;
        }

        public static bool EsInstanceTypeValido(ProveedorNube proveedor, string instanceType)
        {
            return ObtenerEspecificacion(proveedor, instanceType) != null;
        }

        public static List<string> ObtenerInstanceTypesDisponibles(ProveedorNube proveedor)
        {
            var configuraciones = proveedor switch
            {
                ProveedorNube.AWS => _configuracionesAWS,
                ProveedorNube.Azure => _configuracionesAzure,
                ProveedorNube.GCP => _configuracionesGCP,
                ProveedorNube.OnPremise => _configuracionesOnPremise,
                _ => new Dictionary<string, EspecificacionInstancia>()
            };

            return configuraciones.Keys.ToList();
        }
    }
}
