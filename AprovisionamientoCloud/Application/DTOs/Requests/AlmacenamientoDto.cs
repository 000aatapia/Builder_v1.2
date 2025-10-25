using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests
{
    public class AlmacenamientoDto
    {
        public string Region { get; set; } = string.Empty;
        public int? Iops { get; set; }
        public int? CapacidadGB { get; set; }
    }
}
