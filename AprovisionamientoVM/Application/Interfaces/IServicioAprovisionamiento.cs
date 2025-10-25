using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServicioAprovisionamiento
    {
 
        Task<RespuestaAprovisionamientoDto> AprovisionarMaquinaVirtualAsync(SolicitudAprovisionamientoDto solicitud);
        Task<List<DetallesMaquinaVirtualDto>> ObtenerTodasLasMaquinasVirtualesAsync();
        Task<DetallesMaquinaVirtualDto?> ObtenerMaquinaVirtualPorIdAsync(string id);
    }
}
