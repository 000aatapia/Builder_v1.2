using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DetalleAlmacenamientoDto
    {
        public string Id { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public int IOPS { get; set; }
    }
}
