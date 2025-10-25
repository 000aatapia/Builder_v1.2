using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Builders
{
    public class ConstructorMaquinaVirtual : IConstructorMaquinaVirtual
    {
        private MaquinaVirtual _maquinaVirtual;
        private Red? _red;
        private Almacenamiento? _almacenamiento;

        public ConstructorMaquinaVirtual()
        {
            _maquinaVirtual = new MaquinaVirtual();
        }

        public IConstructorMaquinaVirtual EstablecerProveedor(ProveedorNube proveedor)
        {
            _maquinaVirtual.Provider = proveedor;
            return this;
        }

        public IConstructorMaquinaVirtual EstablecerRecursosComputo(int vcpus, int memoryGB)
        {
            _maquinaVirtual.VCpus = vcpus;
            _maquinaVirtual.MemoryGB = memoryGB;
            return this;
        }

        public IConstructorMaquinaVirtual EstablecerNombreInstancia(string nombreInstancia)
        {
            _maquinaVirtual.NombreInstancia = nombreInstancia;
            return this;
        }

        public IConstructorMaquinaVirtual EstablecerTipoMaquina(TipoMaquina tipoMaquina)
        {
            _maquinaVirtual.TipoMaquina = tipoMaquina;
            return this;
        }

        public IConstructorMaquinaVirtual EstablecerOptimizaciones(bool memoryOptimization, bool diskOptimization)
        {
            _maquinaVirtual.MemoryOptimization = memoryOptimization;
            _maquinaVirtual.DiskOptimization = diskOptimization;
            return this;
        }

        public IConstructorMaquinaVirtual EstablecerKeyPair(string keyPairName)
        {
            _maquinaVirtual.KeyPairName = keyPairName;
            return this;
        }

        public IConstructorMaquinaVirtual AgregarRed(Red red)
        {
            _red = red;
            return this;
        }

        public IConstructorMaquinaVirtual AgregarAlmacenamiento(Almacenamiento almacenamiento)
        {
            _almacenamiento = almacenamiento;
            return this;
        }

        public ConfiguracionInfraestructura Construir()
        {
            if (_red == null)
                throw new InvalidOperationException("Debe agregar una configuración de red antes de construir");

            if (_almacenamiento == null)
                throw new InvalidOperationException("Debe agregar una configuración de almacenamiento antes de construir");

            var configuracion = new ConfiguracionInfraestructura(
                _maquinaVirtual,
                _red,
                _almacenamiento
            );

            return configuracion;
        }

        public void Reiniciar()
        {
            _maquinaVirtual = new MaquinaVirtual();
            _red = null;
            _almacenamiento = null;
        }
    }
}
