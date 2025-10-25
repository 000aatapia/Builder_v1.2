using Application.Builders;
using Application.Directors;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Factories;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServicioAprovisionamiento : IServicioAprovisionamiento
    {
        private readonly IRepositorioInfraestructura _repositorio;
        private readonly ServicioValidacion _servicioValidacion;
        private readonly Dictionary<ProveedorNube, IFactoriaRecursos> _factorias;

        public ServicioAprovisionamiento(IRepositorioInfraestructura repositorio)
        {
            _repositorio = repositorio;
            _servicioValidacion = new ServicioValidacion();

            _factorias = new Dictionary<ProveedorNube, IFactoriaRecursos>
        {
            { ProveedorNube.AWS, new FactoriaRecursosAWS() },
            { ProveedorNube.Azure, new FactoriaRecursosAzure() },
            { ProveedorNube.GCP, new FactoriaRecursosGCP() },
            { ProveedorNube.OnPremise, new FactoriaRecursosOnPremise() }
        };
        }

        public async Task<ResultadoOperacion<InfraestructuraResponse>> CrearInfraestructuraAsync(CrearInfraestructuraRequest request)
        {
            var erroresValidacion = _servicioValidacion.ValidarSolicitud(request);
            if (erroresValidacion.Any())
            {
                return ResultadoOperacion<InfraestructuraResponse>.Error(
                    "Errores de validación en la solicitud",
                    erroresValidacion);
            }

            var proveedor = Enum.Parse<ProveedorNube>(request.Provider, true);
            var tipoMaquina = Enum.Parse<TipoMaquina>(request.TipoMaquina, true);
            var tamañoInstancia = Enum.Parse<TamañoInstancia>(request.TamañoInstancia, true);

            var factoria = _factorias[proveedor];

            if (!factoria.ValidarRegion(request.Red.Region))
            {
                return ResultadoOperacion<InfraestructuraResponse>.Error(
                    $"La región '{request.Red.Region}' no es válida para el proveedor {proveedor}");
            }

            if (!factoria.ValidarRegion(request.Almacenamiento.Region))
            {
                return ResultadoOperacion<InfraestructuraResponse>.Error(
                    $"La región '{request.Almacenamiento.Region}' no es válida para el proveedor {proveedor}");
            }

            var red = factoria.CrearRed(
                request.Red.Region,
                request.Red.FirewallRules ?? request.FirewallRulesAdicionales,
                request.Red.PublicIP);

            var almacenamiento = factoria.CrearAlmacenamiento(
                request.Almacenamiento.Region,
                request.Almacenamiento.Iops,
                request.Almacenamiento.CapacidadGB);

            var constructor = new ConstructorMaquinaVirtual();
            var director = new DirectorMaquinaVirtual(constructor);

            ConfiguracionInfraestructura configuracion;
            try
            {
                configuracion = director.ConstruirMaquinaVirtual(
                    proveedor,
                    tipoMaquina,
                    tamañoInstancia,
                    red,
                    almacenamiento,
                    request.KeyPairName);
            }
            catch (InvalidOperationException ex)
            {
                return ResultadoOperacion<InfraestructuraResponse>.Error(ex.Message);
            }

            var erroresCoherenciaProveedor = _servicioValidacion.ValidarCoherenciaProveedor(configuracion);
            if (erroresCoherenciaProveedor.Any())
            {
                return ResultadoOperacion<InfraestructuraResponse>.Error(
                    "Error de coherencia de proveedor",
                    erroresCoherenciaProveedor);
            }

            var erroresCoherenciaRegion = _servicioValidacion.ValidarCoherenciaRegion(configuracion);
            if (erroresCoherenciaRegion.Any())
            {
                return ResultadoOperacion<InfraestructuraResponse>.Error(
                    "Error de coherencia de región",
                    erroresCoherenciaRegion);
            }
            var configuracionGuardada = await _repositorio.AgregarAsync(configuracion);

            var response = MapearAResponse(configuracionGuardada);

            return ResultadoOperacion<InfraestructuraResponse>.Exito(
                response,
                "Infraestructura creada exitosamente");
        }

        public async Task<ResultadoOperacion<InfraestructuraResponse>> ObtenerInfraestructuraPorIdAsync(string id)
        {
            var configuracion = await _repositorio.ObtenerPorIdAsync(id);

            if (configuracion == null)
            {
                return ResultadoOperacion<InfraestructuraResponse>.Error(
                    $"No se encontró infraestructura con ID: {id}");
            }

            var response = MapearAResponse(configuracion);
            return ResultadoOperacion<InfraestructuraResponse>.Exito(response);
        }

        public async Task<ResultadoOperacion<List<InfraestructuraResponse>>> ObtenerTodasInfraestructurasAsync()
        {
            var configuraciones = await _repositorio.ObtenerTodasAsync();
            var responses = configuraciones.Select(MapearAResponse).ToList();

            return ResultadoOperacion<List<InfraestructuraResponse>>.Exito(
                responses,
                $"Se encontraron {responses.Count} infraestructuras");
        }

        public async Task<ResultadoOperacion<Dictionary<string, Dictionary<string, Dictionary<string, string>>>>> ObtenerCatalogoInstanciasAsync()
        {
            await Task.CompletedTask;
            var catalogo = CatalogoInstancias.ObtenerCatalogoCompleto();
            return ResultadoOperacion<Dictionary<string, Dictionary<string, Dictionary<string, string>>>>.Exito(
                catalogo,
                "Catálogo de instancias obtenido exitosamente");
        }
        private InfraestructuraResponse MapearAResponse(ConfiguracionInfraestructura configuracion)
        {
            return new InfraestructuraResponse
            {
                Id = configuracion.Id,
                Provider = configuracion.Provider.ToString(),
                TipoMaquina = configuracion.TipoMaquina.ToString(),
                FechaCreacion = configuracion.FechaCreacion,
                Estado = configuracion.Estado,
                MaquinaVirtual = new MaquinaVirtualResponse
                {
                    Id = configuracion.MaquinaVirtual.Id,
                    NombreInstancia = configuracion.MaquinaVirtual.NombreInstancia ?? "",
                    VCpus = configuracion.MaquinaVirtual.VCpus,
                    MemoryGB = configuracion.MaquinaVirtual.MemoryGB,
                    MemoryOptimization = configuracion.MaquinaVirtual.MemoryOptimization,
                    DiskOptimization = configuracion.MaquinaVirtual.DiskOptimization,
                    KeyPairName = configuracion.MaquinaVirtual.KeyPairName ?? "default-key"
                },
                Red = new RedResponse
                {
                    Id = configuracion.Red.Id,
                    Region = configuracion.Red.Region,
                    FirewallRules = configuracion.Red.FirewallRules ?? new List<string>(),
                    PublicIP = configuracion.Red.PublicIP
                },
                Almacenamiento = new AlmacenamientoResponse
                {
                    Id = configuracion.Almacenamiento.Id,
                    Region = configuracion.Almacenamiento.Region,
                    Iops = configuracion.Almacenamiento.Iops,
                    CapacidadGB = configuracion.Almacenamiento.CapacidadGB
                }
            };
        }
    }


}