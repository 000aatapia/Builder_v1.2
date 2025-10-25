using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Factories
{
    public interface IFactoriaRecursos
    {
        Red CrearRed(string region, List<string>? firewallRules, bool publicIP);
        Almacenamiento CrearAlmacenamiento(string region, int? iops, int? capacidadGB);
        bool ValidarRegion(string region);
        string ObtenerNombreProveedor();
    }
}
