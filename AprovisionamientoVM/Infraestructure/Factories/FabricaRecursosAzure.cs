using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Factories
{
    public class FabricaRecursosAzure : IFabricaRecursos
    {
        public Red CrearRed(ConfiguracionRed configuracion)
        {
            return new Red(ProveedorNube.Azure, configuracion);
        }

        public Almacenamiento CrearAlmacenamiento(ConfiguracionAlmacenamiento configuracion)
        {
            return new Almacenamiento(ProveedorNube.Azure, configuracion);
        }
    }
}
