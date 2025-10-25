using Application.DTOs.Requests;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServicioValidacion
    {
        public List<string> ValidarSolicitud(CrearInfraestructuraRequest request)
        {
            var errores = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Provider))
            {
                errores.Add("El proveedor es obligatorio");
            }
            else if (!Enum.TryParse<ProveedorNube>(request.Provider, true, out _))
            {
                errores.Add($"Proveedor '{request.Provider}' no es válido. Valores permitidos: AWS, Azure, GCP, OnPremise");
            }

            if (string.IsNullOrWhiteSpace(request.TipoMaquina))
            {
                errores.Add("El tipo de máquina es obligatorio");
            }
            else if (!Enum.TryParse<TipoMaquina>(request.TipoMaquina, true, out _))
            {
                errores.Add($"Tipo de máquina '{request.TipoMaquina}' no es válido. Valores permitidos: Standard, MemoryOptimized, ComputeOptimized");
            }

            if (string.IsNullOrWhiteSpace(request.TamañoInstancia))
            {
                errores.Add("El tamaño de instancia es obligatorio");
            }
            else if (!Enum.TryParse<TamañoInstancia>(request.TamañoInstancia, true, out _))
            {
                errores.Add($"Tamaño de instancia '{request.TamañoInstancia}' no es válido. Valores permitidos: Small, Medium, Large");
            }

            if (request.Red == null)
            {
                errores.Add("La configuración de red es obligatoria");
            }
            else if (string.IsNullOrWhiteSpace(request.Red.Region))
            {
                errores.Add("La región de la red es obligatoria");
            }

            if (request.Almacenamiento == null)
            {
                errores.Add("La configuración de almacenamiento es obligatoria");
            }
            else if (string.IsNullOrWhiteSpace(request.Almacenamiento.Region))
            {
                errores.Add("La región del almacenamiento es obligatoria");
            }

            if (request.Red != null && request.Almacenamiento != null)
            {
                if (!string.IsNullOrWhiteSpace(request.Red.Region) &&
                    !string.IsNullOrWhiteSpace(request.Almacenamiento.Region))
                {
                    if (request.Red.Region != request.Almacenamiento.Region)
                    {
                        errores.Add($"La red y el almacenamiento deben estar en la misma región. Red: {request.Red.Region}, Almacenamiento: {request.Almacenamiento.Region}");
                    }
                }
            }

            return errores;
        }

        public List<string> ValidarCoherenciaProveedor(ConfiguracionInfraestructura configuracion)
        {
            var errores = new List<string>();

            var proveedorVM = configuracion.MaquinaVirtual.Provider;
            var proveedorRed = configuracion.Red.Provider;
            var proveedorAlmacenamiento = configuracion.Almacenamiento.Provider;

            if (proveedorVM != proveedorRed)
            {
                errores.Add($"El proveedor de la VM ({proveedorVM}) no coincide con el proveedor de la red ({proveedorRed})");
            }

            if (proveedorVM != proveedorAlmacenamiento)
            {
                errores.Add($"El proveedor de la VM ({proveedorVM}) no coincide con el proveedor del almacenamiento ({proveedorAlmacenamiento})");
            }

            return errores;
        }

        public List<string> ValidarCoherenciaRegion(ConfiguracionInfraestructura configuracion)
        {
            var errores = new List<string>();

            var regionRed = configuracion.Red.Region;
            var regionAlmacenamiento = configuracion.Almacenamiento.Region;

            if (regionRed != regionAlmacenamiento)
            {
                errores.Add($"La región de la red ({regionRed}) debe coincidir con la región del almacenamiento ({regionAlmacenamiento})");
            }

            return errores;
        }
    }
}
