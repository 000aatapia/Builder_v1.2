using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositorioInfraestructura
    {
        Task<ConfiguracionInfraestructura> AgregarAsync(ConfiguracionInfraestructura configuracion);
        Task<ConfiguracionInfraestructura?> ObtenerPorIdAsync(string id);
        Task<List<ConfiguracionInfraestructura>> ObtenerTodasAsync();
        Task<bool> EliminarAsync(string id);
    }
}
