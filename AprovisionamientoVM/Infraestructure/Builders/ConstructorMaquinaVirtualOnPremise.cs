using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Infraestructure.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Builders
{
    public class ConstructorMaquinaVirtualOnPremise : IConstructorMaquinaVirtual
    {
        private readonly IFabricaRecursos _fabrica;

        private string? _nombre;
        private TipoMaquina _tipo;
        private int _vCpus;
        private int _memoriaGB;
        private bool _optimizacionMemoria;
        private bool _optimizacionDisco;
        private string? _claveSSH;
        private Red? _red;
        private Almacenamiento? _almacenamiento;

        public ConstructorMaquinaVirtualOnPremise(IFabricaRecursos fabrica)
        {
            _fabrica = fabrica;
            Reiniciar();
        }

        public IConstructorMaquinaVirtual EstablecerNombre(string nombre)
        {
            _nombre = nombre;
            return this;
        }

        public IConstructorMaquinaVirtual EstablecerTipoMaquina(TipoMaquina tipo)
        {
            _tipo = tipo;
            return this;
        }

        public IConstructorMaquinaVirtual EstablecerRecursosComputo(int vCpus, int memoriaGB)
        {
            _vCpus = vCpus;
            _memoriaGB = memoriaGB;
            return this;
        }

        public IConstructorMaquinaVirtual EstablecerOptimizaciones(bool optimizacionMemoria, bool optimizacionDisco)
        {
            _optimizacionMemoria = optimizacionMemoria;
            _optimizacionDisco = optimizacionDisco;
            return this;
        }

        public IConstructorMaquinaVirtual EstablecerClaveSSH(string? claveSSH)
        {
            _claveSSH = claveSSH;
            return this;
        }

        public IConstructorMaquinaVirtual CrearRed(string region, List<string>? reglasFirewall, bool ipPublica)
        {
            var configuracion = new ConfiguracionRed(region, reglasFirewall, ipPublica);
            _red = _fabrica.CrearRed(configuracion);
            return this;
        }

        public IConstructorMaquinaVirtual CrearAlmacenamiento(string region, int? iops)
        {
            var configuracion = new ConfiguracionAlmacenamiento(region, iops);
            _almacenamiento = _fabrica.CrearAlmacenamiento(configuracion);
            return this;
        }

        public void Reiniciar()
        {
            _nombre = null;
            _tipo = TipoMaquina.Standard;
            _vCpus = 2;
            _memoriaGB = 4;
            _optimizacionMemoria = false;
            _optimizacionDisco = false;
            _claveSSH = "default-key";
            _red = null;
            _almacenamiento = null;
        }

        public MaquinaVirtual Construir()
        {
            if (string.IsNullOrEmpty(_nombre))
                throw new InvalidOperationException("El nombre de la máquina virtual es obligatorio");

            if (_red == null)
                throw new InvalidOperationException("La red es obligatoria");

            if (_almacenamiento == null)
                throw new InvalidOperationException("El almacenamiento es obligatorio");

            var configuracionVM = new ConfiguracionVM(
                ProveedorNube.OnPremise,
                _vCpus,
                _memoriaGB,
                _optimizacionMemoria,
                _optimizacionDisco,
                _claveSSH
            );

            var maquina = new MaquinaVirtual(
                _nombre,
                _tipo,
                configuracionVM,
                _red,
                _almacenamiento
            );

            Reiniciar();
            return maquina;
        }
    }
}
