using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Builders
{
    public interface IConstructorMaquinaVirtual
    {
        IConstructorMaquinaVirtual EstablecerNombre(string nombre);
        IConstructorMaquinaVirtual EstablecerTipoMaquina(TipoMaquina tipo);
        IConstructorMaquinaVirtual EstablecerRecursosComputo(int vCpus, int memoriaGB);
        IConstructorMaquinaVirtual EstablecerOptimizaciones(bool optimizacionMemoria, bool optimizacionDisco);
        IConstructorMaquinaVirtual EstablecerClaveSSH(string? claveSSH);
        IConstructorMaquinaVirtual CrearRed(string region, List<string>? reglasFirewall, bool ipPublica);
        IConstructorMaquinaVirtual CrearAlmacenamiento(string region, int? iops);
        void Reiniciar();
        MaquinaVirtual Construir();
    }
}
