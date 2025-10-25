using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Infraestructure.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Directors
{
    public class DirectorAprovisionamiento
    {
        private readonly IConstructorMaquinaVirtual _constructor;

        public DirectorAprovisionamiento(IConstructorMaquinaVirtual constructor)
        {
            _constructor = constructor;
        }

        public MaquinaVirtual ConstruirMaquinaVirtual(
            SolicitudAprovisionamientoDto solicitud,
            ProveedorNube proveedor,
            TipoMaquina tipo)
        {
            var especificacion = ConfiguracionesTipoMaquina.ObtenerEspecificacion(proveedor, solicitud.InstanceType);

            if (especificacion == null)
            {
                throw new ArgumentException(
                    $"El tipo de instancia '{solicitud.InstanceType}' no es válido para el proveedor {proveedor}. " +
                    $"Tipos válidos: {string.Join(", ", ConfiguracionesTipoMaquina.ObtenerInstanceTypesDisponibles(proveedor))}");
            }

            bool optimizacionMemoria = tipo == TipoMaquina.OptimizadaMemoria || solicitud.OptimizacionMemoria;
            bool optimizacionDisco = tipo == TipoMaquina.OptimizadaDisco || solicitud.OptimizacionDisco;

            _constructor
                .EstablecerNombre(solicitud.Nombre)
                .EstablecerTipoMaquina(tipo)
                .EstablecerRecursosComputo(especificacion.VCpus, especificacion.MemoriaGB)
                .EstablecerOptimizaciones(optimizacionMemoria, optimizacionDisco)
                .EstablecerClaveSSH(solicitud.ClaveSSH)
                .CrearRed(
                    solicitud.Red.Region,
                    solicitud.Red.ReglasFirewall,
                    solicitud.Red.IPPublica)
                .CrearAlmacenamiento(
                    solicitud.Almacenamiento.Region,
                    solicitud.Almacenamiento.IOPS);

            return _constructor.Construir();
        }


        public MaquinaVirtual ConstruirMaquinaStandard(
            SolicitudAprovisionamientoDto solicitud,
            ProveedorNube proveedor)
        {
            return ConstruirMaquinaVirtual(solicitud, proveedor, TipoMaquina.Standard);
        }


        public MaquinaVirtual ConstruirMaquinaOptimizadaMemoria(
            SolicitudAprovisionamientoDto solicitud,
            ProveedorNube proveedor)
        {
            return ConstruirMaquinaVirtual(solicitud, proveedor, TipoMaquina.OptimizadaMemoria);
        }

        public MaquinaVirtual ConstruirMaquinaOptimizadaDisco(
            SolicitudAprovisionamientoDto solicitud,
            ProveedorNube proveedor)
        {
            return ConstruirMaquinaVirtual(solicitud, proveedor, TipoMaquina.OptimizadaDisco);
        }
    }
}
