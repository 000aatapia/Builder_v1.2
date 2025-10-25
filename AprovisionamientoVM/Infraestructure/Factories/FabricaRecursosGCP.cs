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
    public class FabricaRecursosGCP : IFabricaRecursos
    {
        public Red CrearRed(ConfiguracionRed configuracion)
        {
            return new Red(ProveedorNube.GCP, configuracion);
        }

        public Almacenamiento CrearAlmacenamiento(ConfiguracionAlmacenamiento configuracion)
        {
            return new Almacenamiento(ProveedorNube.GCP, configuracion);
        }
    }
}
