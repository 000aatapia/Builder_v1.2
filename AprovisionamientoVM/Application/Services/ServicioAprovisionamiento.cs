using Application.DTOs;
using Application.Interfaces;
using Application.Validators;
using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServicioAprovisionamiento : IServicioAprovisionamiento
    {
        private readonly SolicitudAprovisionamientoValidator _validator;
        private readonly IValidadorCoherencia _validadorCoherencia;
        private readonly List<MaquinaVirtual> _maquinasVirtuales;

        public ServicioAprovisionamiento(
            SolicitudAprovisionamientoValidator validator,
            IValidadorCoherencia validadorCoherencia)
        {
            _validator = validator;
            _validadorCoherencia = validadorCoherencia;
            _maquinasVirtuales = new List<MaquinaVirtual>();
        }

        public async Task<RespuestaAprovisionamientoDto> AprovisionarMaquinaVirtualAsync(
            SolicitudAprovisionamientoDto solicitud)
        {
            try
            {
                var erroresValidacion = _validator.ValidarSolicitud(solicitud);
                var erroresCoherencia = _validadorCoherencia.ValidarSolicitud(solicitud);

                var todosLosErrores = erroresValidacion.Concat(erroresCoherencia).ToList();

                if (todosLosErrores.Any())
                {
                    return new RespuestaAprovisionamientoDto
                    {
                        Exitoso = false,
                        Mensaje = "La solicitud contiene errores de validación",
                        Errores = todosLosErrores
                    };
                }

                return await Task.FromResult(new RespuestaAprovisionamientoDto
                {
                    Exitoso = true,
                    Mensaje = "Validación exitosa. La máquina virtual será creada por el Builder en la capa de Infrastructure",
                    Errores = new List<string>()
                });
            }
            catch (ValidacionDominioException ex)
            {
                return new RespuestaAprovisionamientoDto
                {
                    Exitoso = false,
                    Mensaje = "Error de validación del dominio",
                    Errores = new List<string> { ex.Message }
                };
            }
            catch (Exception ex)
            {
                return new RespuestaAprovisionamientoDto
                {
                    Exitoso = false,
                    Mensaje = "Error inesperado durante el aprovisionamiento",
                    Errores = new List<string> { ex.Message }
                };
            }
        }

        public async Task<List<DetallesMaquinaVirtualDto>> ObtenerTodasLasMaquinasVirtualesAsync()
        {
            return await Task.FromResult(
                _maquinasVirtuales.Select(MapearADto).ToList()
            );
        }

        public async Task<DetallesMaquinaVirtualDto?> ObtenerMaquinaVirtualPorIdAsync(string id)
        {
            var vm = _maquinasVirtuales.FirstOrDefault(v => v.Id == id);
            return await Task.FromResult(vm != null ? MapearADto(vm) : null);
        }

        private DetallesMaquinaVirtualDto MapearADto(MaquinaVirtual vm)
        {
            return new DetallesMaquinaVirtualDto
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Tipo = vm.Tipo.ToString(),
                Proveedor = vm.Configuracion.Proveedor.ToString(),
                Estado = vm.Estado,
                FechaCreacion = vm.FechaCreacion,
                VCpus = vm.Configuracion.VCpus,
                MemoriaGB = vm.Configuracion.MemoriaGB,
                OptimizacionMemoria = vm.Configuracion.OptimizacionMemoria,
                OptimizacionDisco = vm.Configuracion.OptimizacionDisco,
                ClaveSSH = vm.Configuracion.ClaveSSH,
                Red = new DetalleRedDto
                {
                    Id = vm.Red.Id,
                    Region = vm.Red.Configuracion.Region,
                    ReglasFirewall = vm.Red.Configuracion.ReglasFirewall ?? new List<string>(),
                    IPPublica = vm.Red.Configuracion.IPPublica
                },
                Almacenamiento = new DetalleAlmacenamientoDto
                {
                    Id = vm.Almacenamiento.Id,
                    Region = vm.Almacenamiento.Configuracion.Region,
                    IOPS = vm.Almacenamiento.Configuracion.IOPS ?? 3000
                },
                Resumen = vm.ObtenerResumen()
            };
        }
    }
}
