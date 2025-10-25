using Domain.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public abstract class RecursoInfraestructura
    {
        public Guid Id { get; protected set; }
        public ProveedorCloud Proveedor { get; protected set; }
        public string Region { get; protected set; }
        public DateTime FechaCreacion { get; protected set; }

        protected RecursoInfraestructura()
        {
            Id = Guid.NewGuid();
            FechaCreacion = DateTime.UtcNow;
        }

        public void EstablecerProveedor(ProveedorCloud proveedor)
        {
            Proveedor = proveedor;
        }

        public void EstablecerRegion(string region)
        {
            if (string.IsNullOrWhiteSpace(region))
                throw new ArgumentException("La región no puede estar vacía", nameof(region));

            Region = region;
        }
    }
}
