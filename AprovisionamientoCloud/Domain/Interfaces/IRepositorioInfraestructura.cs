using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositorioInfraestructura
    {

        Task<VirtualMachine> GuardarVirtualMachine(VirtualMachine vm);
        Task<VirtualMachine?> ObtenerVirtualMachinePorId(Guid id);
        Task<List<VirtualMachine>> ObtenerTodasLasVirtualMachines();
        Task<bool> EliminarVirtualMachine(Guid id);
    }
}
