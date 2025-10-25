using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Factories
{
    public interface IFabricaRecursos
    {
        Red CrearRed(ConfiguracionRed configuracion);
        Almacenamiento CrearAlmacenamiento(ConfiguracionAlmacenamiento configuracion);
    }
}
