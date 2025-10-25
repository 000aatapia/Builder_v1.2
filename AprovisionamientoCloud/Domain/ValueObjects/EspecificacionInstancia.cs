using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class EspecificacionInstancia
    {
        public string NombreInstancia { get; }
        public int VCpus { get; }
        public int MemoriaGB { get; }

        public EspecificacionInstancia(string nombreInstancia, int vCpus, int memoriaGB)
        {
            if (string.IsNullOrWhiteSpace(nombreInstancia))
                throw new ArgumentException("El nombre de instancia no puede estar vacío", nameof(nombreInstancia));

            if (vCpus <= 0)
                throw new ArgumentException("Los vCPUs deben ser mayor a 0", nameof(vCpus));

            if (memoriaGB <= 0)
                throw new ArgumentException("La memoria debe ser mayor a 0", nameof(memoriaGB));

            NombreInstancia = nombreInstancia;
            VCpus = vCpus;
            MemoriaGB = memoriaGB;
        }

        public override string ToString()
        {
            return $"{NombreInstancia} ({VCpus} vCPU, {MemoriaGB} GiB RAM)";
        }
    }
}
