using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses
{
    public class ResultadoOperacion<T>
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public T? Datos { get; set; }
        public List<string> Errores { get; set; } = new();
        public static ResultadoOperacion<T> Exito(T datos, string mensaje = "Operación exitosa")
        {
            return new ResultadoOperacion<T>
            {
                Exitoso = true,
                Mensaje = mensaje,
                Datos = datos
            };
        }

        public static ResultadoOperacion<T> Error(string mensaje, List<string>? errores = null)
        {
            return new ResultadoOperacion<T>
            {
                Exitoso = false,
                Mensaje = mensaje,
                Errores = errores ?? new List<string>()
            };
        }
    }
}
