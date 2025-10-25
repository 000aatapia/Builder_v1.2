using Domain.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class VirtualMachine : RecursoInfraestructura
    {
        public int VCpus { get; private set; }
        public int MemoryGB { get; private set; }

        public bool? MemoryOptimization { get; private set; }
        public bool? DiskOptimization { get; private set; }
        public string? KeyPairName { get; private set; }

        public Network Network { get; private set; }
        public Storage Storage { get; private set; }

        public TipoMaquina TipoMaquina { get; private set; }
        public string NombreInstancia { get; private set; }

        public VirtualMachine()
        {
        }

        public void EstablecerVCpus(int vcpus)
        {
            if (vcpus <= 0)
                throw new ArgumentException("Los vCPUs deben ser mayor a 0", nameof(vcpus));

            VCpus = vcpus;
        }

        public void EstablecerMemoria(int memoryGB)
        {
            if (memoryGB <= 0)
                throw new ArgumentException("La memoria debe ser mayor a 0 GB", nameof(memoryGB));

            MemoryGB = memoryGB;
        }

        public void EstablecerOptimizacionMemoria(bool? optimizacion)
        {
            MemoryOptimization = optimizacion;
        }


        public void EstablecerOptimizacionDisco(bool? optimizacion)
        {
            DiskOptimization = optimizacion;
        }


        public void EstablecerKeyPairName(string? keyPairName)
        {
            KeyPairName = keyPairName;
        }

        public void AsociarNetwork(Network network)
        {
            Network = network ?? throw new ArgumentNullException(nameof(network));
        }

        public void AsociarStorage(Storage storage)
        {
            Storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public void EstablecerTipoMaquina(TipoMaquina tipo)
        {
            TipoMaquina = tipo;
        }

        public void EstablecerNombreInstancia(string nombreInstancia)
        {
            if (string.IsNullOrWhiteSpace(nombreInstancia))
                throw new ArgumentException("El nombre de instancia no puede estar vacío", nameof(nombreInstancia));

            NombreInstancia = nombreInstancia;
        }
    }
}
