using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public static class AlmacenamientoEnMemoria
    {
       
        private static readonly List<ConfiguracionInfraestructura> _configuraciones = new();
        private static readonly object _lock = new();
        public static ConfiguracionInfraestructura Agregar(ConfiguracionInfraestructura configuracion)
        {
            lock (_lock)
            {
                _configuraciones.Add(configuracion);
                return configuracion;
            }
        }

        public static ConfiguracionInfraestructura? ObtenerPorId(string id)
        {
            lock (_lock)
            {
                return _configuraciones.FirstOrDefault(c => c.Id == id);
            }
        }

        public static List<ConfiguracionInfraestructura> ObtenerTodas()
        {
            lock (_lock)
            {
                return _configuraciones.ToList();
            }
        }

        public static bool Eliminar(string id)
        {
            lock (_lock)
            {
                var configuracion = _configuraciones.FirstOrDefault(c => c.Id == id);
                if (configuracion != null)
                {
                    _configuraciones.Remove(configuracion);
                    return true;
                }
                return false;
            }
        }

        public static void LimpiarTodo()
        {
            lock (_lock)
            {
                _configuraciones.Clear();
            }
        }

        public static int ObtenerConteo()
        {
            lock (_lock)
            {
                return _configuraciones.Count;
            }
        }
    }
}
