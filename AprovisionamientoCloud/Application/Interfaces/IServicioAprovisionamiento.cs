using Application.DTOs.Requests;
using Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServicioAprovisionamiento
    {
        Task<ResultadoOperacion<InfraestructuraResponse>> CrearInfraestructuraAsync(CrearInfraestructuraRequest request);
        Task<ResultadoOperacion<InfraestructuraResponse>> ObtenerInfraestructuraPorIdAsync(string id);
        Task<ResultadoOperacion<List<InfraestructuraResponse>>> ObtenerTodasInfraestructurasAsync();
        Task<ResultadoOperacion<Dictionary<string, Dictionary<string, Dictionary<string, string>>>>> ObtenerCatalogoInstanciasAsync();
    }
}
