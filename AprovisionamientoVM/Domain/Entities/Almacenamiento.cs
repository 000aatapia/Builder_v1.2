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
    public class Almacenamiento
    {
        public string Id { get; private set; }
        public ProveedorNube Proveedor { get; private set; }
        public ConfiguracionAlmacenamiento Configuracion { get; private set; }
        public DateTime FechaCreacion { get; private set; }

        public Almacenamiento(
            ProveedorNube proveedor,
            ConfiguracionAlmacenamiento configuracion)
        {
            Id = Guid.NewGuid().ToString();
            Proveedor = proveedor;
            Configuracion = configuracion ?? throw new ValidacionDominioException("La configuración de almacenamiento es obligatoria");
            FechaCreacion = DateTime.UtcNow;
        }
    }
}
