using Application.DTOs;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class SolicitudAprovisionamientoValidator
    {
        private static readonly HashSet<string> ProveedoresValidos = new()
    {
        "AWS", "Azure", "GCP", "OnPremise"
    };

        private static readonly HashSet<string> TiposMaquinaValidos = new()
    {
        "Standard", "OptimizadaMemoria", "OptimizadaDisco"
    };

        public List<string> ValidarSolicitud(SolicitudAprovisionamientoDto solicitud)
        {
            var errores = new List<string>();

            if (string.IsNullOrWhiteSpace(solicitud.Nombre))
            {
                errores.Add("El nombre de la máquina virtual es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(solicitud.Proveedor))
            {
                errores.Add("El proveedor es obligatorio");
            }
            else if (!ProveedoresValidos.Contains(solicitud.Proveedor))
            {
                errores.Add($"Proveedor no válido. Debe ser uno de: {string.Join(", ", ProveedoresValidos)}");
            }

            if (string.IsNullOrWhiteSpace(solicitud.TipoMaquina))
            {
                errores.Add("El tipo de máquina es obligatorio");
            }
            else if (!TiposMaquinaValidos.Contains(solicitud.TipoMaquina))
            {
                errores.Add($"Tipo de máquina no válido. Debe ser uno de: {string.Join(", ", TiposMaquinaValidos)}");
            }

            if (string.IsNullOrWhiteSpace(solicitud.InstanceType))
            {
                errores.Add("El tipo de instancia (InstanceType) es obligatorio");
            }

            if (solicitud.Red == null)
            {
                errores.Add("La configuración de red es obligatoria");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(solicitud.Red.Region))
                {
                    errores.Add("La región de la red es obligatoria");
                }
            }

            if (solicitud.Almacenamiento == null)
            {
                errores.Add("La configuración de almacenamiento es obligatoria");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(solicitud.Almacenamiento.Region))
                {
                    errores.Add("La región del almacenamiento es obligatoria");
                }
            }

            if (solicitud.Red?.Region != null &&
                solicitud.Almacenamiento?.Region != null &&
                solicitud.Red.Region != solicitud.Almacenamiento.Region)
            {
                errores.Add($"La red y el almacenamiento deben estar en la misma región. " +
                           $"Red: {solicitud.Red.Region}, Almacenamiento: {solicitud.Almacenamiento.Region}");
            }

            return errores;
        }

        public ProveedorNube ConvertirProveedorNube(string proveedor)
        {
            return proveedor switch
            {
                "AWS" => ProveedorNube.AWS,
                "Azure" => ProveedorNube.Azure,
                "GCP" => ProveedorNube.GCP,
                "OnPremise" => ProveedorNube.OnPremise,
                _ => throw new ArgumentException($"Proveedor no válido: {proveedor}")
            };
        }

        public TipoMaquina ConvertirTipoMaquina(string tipoMaquina)
        {
            return tipoMaquina switch
            {
                "Standard" => TipoMaquina.Standard,
                "OptimizadaMemoria" => TipoMaquina.OptimizadaMemoria,
                "OptimizadaDisco" => TipoMaquina.OptimizadaDisco,
                _ => throw new ArgumentException($"Tipo de máquina no válido: {tipoMaquina}")
            };
        }
    }
}
