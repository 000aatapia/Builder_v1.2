using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DetalleRedDto
    {
        public string Id { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public List<string> ReglasFirewall { get; set; } = new();
        public bool IPPublica { get; set; }
    }
}
