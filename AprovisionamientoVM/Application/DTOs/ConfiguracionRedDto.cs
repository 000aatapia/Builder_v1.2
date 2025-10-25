using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ConfiguracionRedDto
    {
        public string Region { get; set; } = string.Empty;
        public List<string>? ReglasFirewall { get; set; }
        public bool IPPublica { get; set; }
    }

}
