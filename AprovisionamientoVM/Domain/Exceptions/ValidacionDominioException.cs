using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ValidacionDominioException : Exception
    {
        public ValidacionDominioException(string mensaje) : base(mensaje)
        {
        }

        public ValidacionDominioException(string mensaje, Exception excepcionInterna)
            : base(mensaje, excepcionInterna)
        {
        }
    }
}
