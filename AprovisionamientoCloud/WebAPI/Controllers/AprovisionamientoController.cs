using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AprovisionamientoController : ControllerBase
    {
        private readonly IServicioAprovisionamiento _servicioAprovisionamiento;
        private readonly ILogger<AprovisionamientoController> _logger;

        public AprovisionamientoController(
            IServicioAprovisionamiento servicioAprovisionamiento,
            ILogger<AprovisionamientoController> logger)
        {
            _servicioAprovisionamiento = servicioAprovisionamiento;
            _logger = logger;
        }

        [HttpPost("crear")]
        [ProducesResponseType(typeof(ResultadoOperacion<InfraestructuraResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoOperacion<InfraestructuraResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearInfraestructura([FromBody] CrearInfraestructuraRequest request)
        {
            try
            {
                _logger.LogInformation("Solicitud de creación de infraestructura recibida. Proveedor: {Provider}, Tipo: {Tipo}",
                    request.Provider, request.TipoMaquina);

                var resultado = await _servicioAprovisionamiento.CrearInfraestructuraAsync(request);

                if (!resultado.Exitoso)
                {
                    _logger.LogWarning("Error al crear infraestructura: {Mensaje}", resultado.Mensaje);
                    return BadRequest(resultado);
                }

                _logger.LogInformation("Infraestructura creada exitosamente. ID: {Id}", resultado.Datos?.Id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear infraestructura");
                return StatusCode(500, new
                {
                    exitoso = false,
                    mensaje = "Error interno del servidor",
                    error = ex.Message
                });
            }
        }

       
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResultadoOperacion<InfraestructuraResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoOperacion<InfraestructuraResponse>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerPorId(string id)
        {
            try
            {
                _logger.LogInformation("Consultando infraestructura con ID: {Id}", id);

                var resultado = await _servicioAprovisionamiento.ObtenerInfraestructuraPorIdAsync(id);

                if (!resultado.Exitoso)
                {
                    _logger.LogWarning("Infraestructura no encontrada. ID: {Id}", id);
                    return NotFound(resultado);
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener infraestructura con ID: {Id}", id);
                return StatusCode(500, new
                {
                    exitoso = false,
                    mensaje = "Error interno del servidor",
                    error = ex.Message
                });
            }
        }

        [HttpGet("listar")]
        [ProducesResponseType(typeof(ResultadoOperacion<List<InfraestructuraResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerTodas()
        {
            try
            {
                _logger.LogInformation("Consultando todas las infraestructuras");

                var resultado = await _servicioAprovisionamiento.ObtenerTodasInfraestructurasAsync();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las infraestructuras");
                return StatusCode(500, new
                {
                    exitoso = false,
                    mensaje = "Error interno del servidor",
                    error = ex.Message
                });
            }
        }

        [HttpGet("catalogo")]
        [ProducesResponseType(typeof(ResultadoOperacion<Dictionary<string, Dictionary<string, Dictionary<string, string>>>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerCatalogo()
        {
            try
            {
                _logger.LogInformation("Consultando catálogo de instancias");

                var resultado = await _servicioAprovisionamiento.ObtenerCatalogoInstanciasAsync();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener catálogo de instancias");
                return StatusCode(500, new
                {
                    exitoso = false,
                    mensaje = "Error interno del servidor",
                    error = ex.Message
                });
            }
        }

        [HttpGet("health")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Health()
        {
            return Ok(new
            {
                estado = "Saludable",
                servicio = "API de Aprovisionamiento Multi-Cloud",
                timestamp = DateTime.UtcNow
            });
        }
    }
}
