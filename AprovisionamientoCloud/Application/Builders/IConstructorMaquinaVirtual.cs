using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Builders
{
    public interface IConstructorMaquinaVirtual
    {
        IConstructorMaquinaVirtual EstablecerProveedor(ProveedorNube proveedor);
        IConstructorMaquinaVirtual EstablecerRecursosComputo(int vcpus, int memoryGB);
        IConstructorMaquinaVirtual EstablecerNombreInstancia(string nombreInstancia);
        IConstructorMaquinaVirtual EstablecerTipoMaquina(TipoMaquina tipoMaquina);
        IConstructorMaquinaVirtual EstablecerOptimizaciones(bool memoryOptimization, bool diskOptimization);
        IConstructorMaquinaVirtual EstablecerKeyPair(string keyPairName);
        IConstructorMaquinaVirtual AgregarRed(Red red);
        IConstructorMaquinaVirtual AgregarAlmacenamiento(Almacenamiento almacenamiento);
        ConfiguracionInfraestructura Construir();
        void Reiniciar();
    }
}
