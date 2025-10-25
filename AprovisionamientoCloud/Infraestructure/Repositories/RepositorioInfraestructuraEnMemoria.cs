using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class RepositorioInfraestructuraEnMemoria : IRepositorioInfraestructura
    {
        public Task<ConfiguracionInfraestructura> AgregarAsync(ConfiguracionInfraestructura configuracion)
        {
            if (configuracion == null)
            {
                throw new ArgumentNullException(nameof(configuracion), "La configuración no puede ser nula");
            }

            var configuracionAgregada = AlmacenamientoEnMemoria.Agregar(configuracion);
            return Task.FromResult(configuracionAgregada);
        }
        public Task<ConfiguracionInfraestructura?> ObtenerPorIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("El ID no puede estar vacío", nameof(id));
            }

            var configuracion = AlmacenamientoEnMemoria.ObtenerPorId(id);
            return Task.FromResult(configuracion);
        }

        public Task<List<ConfiguracionInfraestructura>> ObtenerTodasAsync()
        {
            var configuraciones = AlmacenamientoEnMemoria.ObtenerTodas();
            return Task.FromResult(configuraciones);
        }
        public Task<bool> EliminarAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("El ID no puede estar vacío", nameof(id));
            }

            var eliminado = AlmacenamientoEnMemoria.Eliminar(id);
            return Task.FromResult(eliminado);
        }
    }
}
