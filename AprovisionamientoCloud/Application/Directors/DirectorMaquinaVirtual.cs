using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Application.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Directors
{
    public class DirectorMaquinaVirtual
    {
        private readonly IConstructorMaquinaVirtual _constructor;

        public DirectorMaquinaVirtual(IConstructorMaquinaVirtual constructor)
        {
            _constructor = constructor;
        }
        public ConfiguracionInfraestructura ConstruirMaquinaVirtual(
            ProveedorNube proveedor,
            TipoMaquina tipoMaquina,
            TamañoInstancia tamañoInstancia,
            Red red,
            Almacenamiento almacenamiento,
            string? keyPairName = null)
        {
            var especificacion = CatalogoInstancias.ObtenerEspecificacion(proveedor, tipoMaquina, tamañoInstancia);

            if (especificacion == null)
            {
                throw new InvalidOperationException(
                    $"No existe especificación para Proveedor: {proveedor}, Tipo: {tipoMaquina}, Tamaño: {tamañoInstancia}");
            }

            bool memoryOptimization = tipoMaquina == TipoMaquina.MemoryOptimized;
            bool diskOptimization = tipoMaquina == TipoMaquina.ComputeOptimized;

            _constructor.Reiniciar();
            _constructor
                .EstablecerProveedor(proveedor)
                .EstablecerTipoMaquina(tipoMaquina)
                .EstablecerNombreInstancia(especificacion.NombreInstancia)
                .EstablecerRecursosComputo(especificacion.VCpus, especificacion.MemoriaGB)
                .EstablecerOptimizaciones(memoryOptimization, diskOptimization)
                .EstablecerKeyPair(keyPairName ?? "default-key")
                .AgregarRed(red)
                .AgregarAlmacenamiento(almacenamiento);

            return _constructor.Construir();
        }

        public ConfiguracionInfraestructura ConstruirMaquinaStandard(
            ProveedorNube proveedor,
            TamañoInstancia tamaño,
            Red red,
            Almacenamiento almacenamiento,
            string? keyPairName = null)
        {
            return ConstruirMaquinaVirtual(proveedor, TipoMaquina.Standard, tamaño, red, almacenamiento, keyPairName);
        }

        public ConfiguracionInfraestructura ConstruirMaquinaOptimizadaMemoria(
            ProveedorNube proveedor,
            TamañoInstancia tamaño,
            Red red,
            Almacenamiento almacenamiento,
            string? keyPairName = null)
        {
            return ConstruirMaquinaVirtual(proveedor, TipoMaquina.MemoryOptimized, tamaño, red, almacenamiento, keyPairName);
        }

        public ConfiguracionInfraestructura ConstruirMaquinaOptimizadaDisco(
            ProveedorNube proveedor,
            TamañoInstancia tamaño,
            Red red,
            Almacenamiento almacenamiento,
            string? keyPairName = null)
        {
            return ConstruirMaquinaVirtual(proveedor, TipoMaquina.ComputeOptimized, tamaño, red, almacenamiento, keyPairName);
        }
    }
}
