using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class RepositorioMaquinasVirtualesEnMemoria
    {
        private readonly List<MaquinaVirtual> _maquinasVirtuales;
        private readonly object _lock = new();

        public RepositorioMaquinasVirtualesEnMemoria()
        {
            _maquinasVirtuales = new List<MaquinaVirtual>();
        }

        public void Agregar(MaquinaVirtual maquinaVirtual)
        {
            lock (_lock)
            {
                _maquinasVirtuales.Add(maquinaVirtual);
            }
        }

        public List<MaquinaVirtual> ObtenerTodas()
        {
            lock (_lock)
            {
                return new List<MaquinaVirtual>(_maquinasVirtuales);
            }
        }

        public MaquinaVirtual? ObtenerPorId(string id)
        {
            lock (_lock)
            {
                return _maquinasVirtuales.FirstOrDefault(vm => vm.Id == id);
            }
        }


        public List<MaquinaVirtual> ObtenerPorProveedor(string proveedor)
        {
            lock (_lock)
            {
                return _maquinasVirtuales
                    .Where(vm => vm.Configuracion.Proveedor.ToString() == proveedor)
                    .ToList();
            }
        }

        public int ObtenerConteo()
        {
            lock (_lock)
            {
                return _maquinasVirtuales.Count;
            }
        }

        public void LimpiarTodo()
        {
            lock (_lock)
            {
                _maquinasVirtuales.Clear();
            }
        }
    }
}
