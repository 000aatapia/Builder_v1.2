using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Storage : RecursoInfraestructura
    {
        public int? Iops { get; private set; }

        public Storage()
        {
        }

        public void EstablecerIops(int? iops)
        {
            if (iops.HasValue && iops.Value <= 0)
                throw new ArgumentException("Los IOPS deben ser mayor a 0", nameof(iops));

            Iops = iops;
        }
    }
}
