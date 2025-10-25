using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MaquinaVirtual
    {
        public string Id { get; private set; }
        public string Nombre { get; private set; }
        public TipoMaquina Tipo { get; private set; }
        public ConfiguracionVM Configuracion { get; private set; }
        public Red Red { get; private set; }
        public Almacenamiento Almacenamiento { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public string Estado { get; private set; }

        public MaquinaVirtual(
            string nombre,
            TipoMaquina tipo,
            ConfiguracionVM configuracion,
            Red red,
            Almacenamiento almacenamiento)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ValidacionDominioException("El nombre de la máquina virtual es obligatorio");

            ValidarCoherenciaProveedor(configuracion.Proveedor, red.Proveedor, almacenamiento.Proveedor);

            ValidarCoherenciaRegion(red.Configuracion.Region, almacenamiento.Configuracion.Region);

            Id = Guid.NewGuid().ToString();
            Nombre = nombre;
            Tipo = tipo;
            Configuracion = configuracion;
            Red = red;
            Almacenamiento = almacenamiento;
            FechaCreacion = DateTime.UtcNow;
            Estado = "Activa";
        }


        private void ValidarCoherenciaProveedor(ProveedorNube proveedorVM, ProveedorNube proveedorRed, ProveedorNube proveedorAlmacenamiento)
        {
            if (proveedorVM != proveedorRed || proveedorVM != proveedorAlmacenamiento)
            {
                throw new IncoherenciaProveedorException(
                    $"Todos los recursos deben pertenecer al mismo proveedor. " +
                    $"VM: {proveedorVM}, Red: {proveedorRed}, Almacenamiento: {proveedorAlmacenamiento}");
            }
        }

        private void ValidarCoherenciaRegion(string regionRed, string regionAlmacenamiento)
        {
            if (regionRed != regionAlmacenamiento)
            {
                throw new IncoherenciaProveedorException(
                    $"La red y el almacenamiento deben estar en la misma región. " +
                    $"Red: {regionRed}, Almacenamiento: {regionAlmacenamiento}");
            }
        }

        public string ObtenerResumen()
        {
            return $"VM: {Nombre} | Tipo: {Tipo} | Proveedor: {Configuracion.Proveedor} | " +
                   $"Recursos: {Configuracion.VCpus} vCPUs, {Configuracion.MemoriaGB} GB RAM | " +
                   $"Región: {Red.Configuracion.Region}";
        }
    }
}