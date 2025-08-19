using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySoft.Domain.Entities
{
    public class HistorialServicio
    {
        public int HistorialServicioId { get; set; }
        public int CitaId { get; set; }
        public string? Observaciones { get; set; }
        public string? ProductosUsados { get; set; }

        public virtual Cita Cita { get; set; } = null!;
    }
}
