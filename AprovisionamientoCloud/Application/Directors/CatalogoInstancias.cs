using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Directors
{
    public static class CatalogoInstancias
    {
        private static readonly Dictionary<ProveedorNube, Dictionary<TipoMaquina, Dictionary<TamañoInstancia, EspecificacionInstancia>>> _catalogo = new()
    {
        // AWS
        {
            ProveedorNube.AWS, new Dictionary<TipoMaquina, Dictionary<TamañoInstancia, EspecificacionInstancia>>
            {
                {
                    TipoMaquina.Standard, new Dictionary<TamañoInstancia, EspecificacionInstancia>
                    {
                        { TamañoInstancia.Small, new EspecificacionInstancia("t3.medium", 2, 4) },
                        { TamañoInstancia.Medium, new EspecificacionInstancia("m5.large", 2, 8) },
                        { TamañoInstancia.Large, new EspecificacionInstancia("m5.xlarge", 4, 16) }
                    }
                },
                {
                    TipoMaquina.MemoryOptimized, new Dictionary<TamañoInstancia, EspecificacionInstancia>
                    {
                        { TamañoInstancia.Small, new EspecificacionInstancia("r5.large", 2, 16) },
                        { TamañoInstancia.Medium, new EspecificacionInstancia("r5.xlarge", 4, 32) },
                        { TamañoInstancia.Large, new EspecificacionInstancia("r5.2xlarge", 8, 64) }
                    }
                },
                {
                    TipoMaquina.ComputeOptimized, new Dictionary<TamañoInstancia, EspecificacionInstancia>
                    {
                        { TamañoInstancia.Small, new EspecificacionInstancia("c5.large", 2, 4) },
                        { TamañoInstancia.Medium, new EspecificacionInstancia("c5.xlarge", 4, 8) },
                        { TamañoInstancia.Large, new EspecificacionInstancia("c5.2xlarge", 8, 16) }
                    }
                }
            }
        },

        {
            ProveedorNube.Azure, new Dictionary<TipoMaquina, Dictionary<TamañoInstancia, EspecificacionInstancia>>
            {
                {
                    TipoMaquina.Standard, new Dictionary<TamañoInstancia, EspecificacionInstancia>
                    {
                        { TamañoInstancia.Small, new EspecificacionInstancia("D2s_v3", 2, 8) },
                        { TamañoInstancia.Medium, new EspecificacionInstancia("D4s_v3", 4, 16) },
                        { TamañoInstancia.Large, new EspecificacionInstancia("D8s_v3", 8, 32) }
                    }
                },
                {
                    TipoMaquina.MemoryOptimized, new Dictionary<TamañoInstancia, EspecificacionInstancia>
                    {
                        { TamañoInstancia.Small, new EspecificacionInstancia("E2s_v3", 2, 16) },
                        { TamañoInstancia.Medium, new EspecificacionInstancia("E4s_v3", 4, 32) },
                        { TamañoInstancia.Large, new EspecificacionInstancia("E8s_v3", 8, 64) }
                    }
                },
                {
                    TipoMaquina.ComputeOptimized, new Dictionary<TamañoInstancia, EspecificacionInstancia>
                    {
                        { TamañoInstancia.Small, new EspecificacionInstancia("F2s_v2", 2, 4) },
                        { TamañoInstancia.Medium, new EspecificacionInstancia("F4s_v2", 4, 8) },
                        { TamañoInstancia.Large, new EspecificacionInstancia("F8s_v2", 8, 16) }
                    }
                }
            }
        },
       
        {
            ProveedorNube.GCP, new Dictionary<TipoMaquina, Dictionary<TamañoInstancia, EspecificacionInstancia>>
            {
                {
                    TipoMaquina.Standard, new Dictionary<TamañoInstancia, EspecificacionInstancia>
                    {
                        { TamañoInstancia.Small, new EspecificacionInstancia("e2-standard-2", 2, 8) },
                        { TamañoInstancia.Medium, new EspecificacionInstancia("e2-standard-4", 4, 16) },
                        { TamañoInstancia.Large, new EspecificacionInstancia("e2-standard-8", 8, 32) }
                    }
                },
                {
                    TipoMaquina.MemoryOptimized, new Dictionary<TamañoInstancia, EspecificacionInstancia>
                    {
                        { TamañoInstancia.Small, new EspecificacionInstancia("n2-highmem-2", 2, 16) },
                        { TamañoInstancia.Medium, new EspecificacionInstancia("n2-highmem-4", 4, 32) },
                        { TamañoInstancia.Large, new EspecificacionInstancia("n2-highmem-8", 8, 64) }
                    }
                },
                {
                    TipoMaquina.ComputeOptimized, new Dictionary<TamañoInstancia, EspecificacionInstancia>
                    {
                        { TamañoInstancia.Small, new EspecificacionInstancia("n2-highcpu-2", 2, 2) },
                        { TamañoInstancia.Medium, new EspecificacionInstancia("n2-highcpu-4", 4, 4) },
                        { TamañoInstancia.Large, new EspecificacionInstancia("n2-highcpu-8", 8, 8) }
                    }
                }
            }
        },
       
        {
            ProveedorNube.OnPremise, new Dictionary<TipoMaquina, Dictionary<TamañoInstancia, EspecificacionInstancia>>
            {
                {
                    TipoMaquina.Standard, new Dictionary<TamañoInstancia, EspecificacionInstancia>
                    {
                        { TamañoInstancia.Small, new EspecificacionInstancia("onprem-std1", 2, 4) },
                        { TamañoInstancia.Medium, new EspecificacionInstancia("onprem-std2", 4, 8) },
                        { TamañoInstancia.Large, new EspecificacionInstancia("onprem-std3", 8, 16) }
                    }
                },
                {
                    TipoMaquina.MemoryOptimized, new Dictionary<TamañoInstancia, EspecificacionInstancia>
                    {
                        { TamañoInstancia.Small, new EspecificacionInstancia("onprem-mem1", 2, 16) },
                        { TamañoInstancia.Medium, new EspecificacionInstancia("onprem-mem2", 4, 32) },
                        { TamañoInstancia.Large, new EspecificacionInstancia("onprem-mem3", 8, 64) }
                    }
                },
                {
                    TipoMaquina.ComputeOptimized, new Dictionary<TamañoInstancia, EspecificacionInstancia>
                    {
                        { TamañoInstancia.Small, new EspecificacionInstancia("onprem-cpu1", 2, 2) },
                        { TamañoInstancia.Medium, new EspecificacionInstancia("onprem-cpu2", 4, 4) },
                        { TamañoInstancia.Large, new EspecificacionInstancia("onprem-cpu3", 8, 8) }
                    }
                }
            }
        }
    };

        public static EspecificacionInstancia? ObtenerEspecificacion(
            ProveedorNube proveedor,
            TipoMaquina tipoMaquina,
            TamañoInstancia tamañoInstancia)
        {
            if (_catalogo.TryGetValue(proveedor, out var tiposPorProveedor))
            {
                if (tiposPorProveedor.TryGetValue(tipoMaquina, out var tamañosPorTipo))
                {
                    if (tamañosPorTipo.TryGetValue(tamañoInstancia, out var especificacion))
                    {
                        return especificacion;
                    }
                }
            }

            return null;
        }

        public static Dictionary<string, Dictionary<string, Dictionary<string, string>>> ObtenerCatalogoCompleto()
        {
            var resultado = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

            foreach (var proveedor in _catalogo.Keys)
            {
                var tiposDict = new Dictionary<string, Dictionary<string, string>>();

                foreach (var tipo in _catalogo[proveedor].Keys)
                {
                    var tamañosDict = new Dictionary<string, string>();

                    foreach (var tamaño in _catalogo[proveedor][tipo].Keys)
                    {
                        var spec = _catalogo[proveedor][tipo][tamaño];
                        tamañosDict[tamaño.ToString()] = spec.ToString();
                    }

                    tiposDict[tipo.ToString()] = tamañosDict;
                }

                resultado[proveedor.ToString()] = tiposDict;
            }

            return resultado;
        }
    }
}
