using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ValidadorCoherencia : IValidadorCoherencia
    {
        private readonly Dictionary<string, HashSet<string>> _regionesPorProveedor = new()
    {
        {
            "AWS", new HashSet<string>
            {
                "us-east-1", "us-west-1", "us-west-2", "eu-west-1",
                "eu-central-1", "ap-southeast-1", "ap-northeast-1"
            }
        },
        {
            "Azure", new HashSet<string>
            {
                "eastus", "westus", "westeurope", "northeurope",
                "southeastasia", "eastasia", "centralus"
            }
        },
        {
            "GCP", new HashSet<string>
            {
                "us-central1", "us-east1", "us-west1", "europe-west1",
                "asia-east1", "asia-southeast1", "australia-southeast1"
            }
        },
        {
            "OnPremise", new HashSet<string>
            {
                "datacenter-1", "datacenter-2", "datacenter-3"
            }
        }
    };

        public List<string> ValidarSolicitud(SolicitudAprovisionamientoDto solicitud)
        {
            var errores = new List<string>();

            if (_regionesPorProveedor.ContainsKey(solicitud.Proveedor))
            {
                var regionesValidas = _regionesPorProveedor[solicitud.Proveedor];

                if (!string.IsNullOrEmpty(solicitud.Red?.Region) &&
                    !regionesValidas.Contains(solicitud.Red.Region))
                {
                    errores.Add($"La región '{solicitud.Red.Region}' no es válida para el proveedor {solicitud.Proveedor}. " +
                               $"Regiones válidas: {string.Join(", ", regionesValidas)}");
                }

                if (!string.IsNullOrEmpty(solicitud.Almacenamiento?.Region) &&
                    !regionesValidas.Contains(solicitud.Almacenamiento.Region))
                {
                    errores.Add($"La región '{solicitud.Almacenamiento.Region}' no es válida para el proveedor {solicitud.Proveedor}. " +
                               $"Regiones válidas: {string.Join(", ", regionesValidas)}");
                }
            }

            if (!string.IsNullOrEmpty(solicitud.Red?.Region) &&
                !string.IsNullOrEmpty(solicitud.Almacenamiento?.Region) &&
                solicitud.Red.Region != solicitud.Almacenamiento.Region)
            {
                errores.Add($"La red y el almacenamiento deben estar en la misma región. " +
                           $"Red: {solicitud.Red.Region}, Almacenamiento: {solicitud.Almacenamiento.Region}");
            }

            if (solicitud.Almacenamiento?.IOPS.HasValue == true)
            {
                var iops = solicitud.Almacenamiento.IOPS.Value;

                if (iops < 0)
                {
                    errores.Add("El valor de IOPS no puede ser negativo");
                }

                switch (solicitud.Proveedor)
                {
                    case "AWS":
                        if (iops > 64000)
                            errores.Add("AWS: El IOPS máximo es 64000");
                        break;
                    case "Azure":
                        if (iops > 80000)
                            errores.Add("Azure: El IOPS máximo es 80000");
                        break;
                    case "GCP":
                        if (iops > 100000)
                            errores.Add("GCP: El IOPS máximo es 100000");
                        break;
                }
            }

            return errores;
        }
    }
}
