using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Excepciones
{
    public class ValidacionProveedorException : Exception
    {
        public ValidacionProveedorException(string mensaje) : base(mensaje)
        {
        }

        public ValidacionProveedorException(string mensaje, Exception innerException)
            : base(mensaje, innerException)
        {
        }
    }
}

