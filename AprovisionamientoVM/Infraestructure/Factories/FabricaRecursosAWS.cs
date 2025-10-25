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
    public class FabricaRecursosAWS : IFabricaRecursos
    {
        public Red CrearRed(ConfiguracionRed configuracion)
        {
            return new Red(ProveedorNube.AWS, configuracion);
        }

        public Almacenamiento CrearAlmacenamiento(ConfiguracionAlmacenamiento configuracion)
        {
            return new Almacenamiento(ProveedorNube.AWS, configuracion);
        }
    }
}
