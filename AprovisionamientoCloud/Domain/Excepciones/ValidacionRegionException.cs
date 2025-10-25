using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Excepciones
{
    public class ValidacionRegionException : Exception
    {
        public ValidacionRegionException(string mensaje) : base(mensaje)
        {
        }

        public ValidacionRegionException(string mensaje, Exception innerException)
            : base(mensaje, innerException)
        {
        }
    }
}
