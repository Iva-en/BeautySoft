using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySoft.Domain.Entities
{
    public class Cita
    {
        public int CitaId { get; set; }
        public int ClienteId { get; set; }
        public int EstilistaId { get; set; }
        public int ServicioId { get; set; }
        public DateTime FechaHora { get; set; }
        public EstadoCita Estado { get; set; } = EstadoCita.Programada;
        public decimal? PrecioFinal { get; set; }
        public string? Notas { get; set; }

        public virtual Cliente Cliente { get; set; } = null!;
        public virtual Estilista Estilista { get; set; } = null!;
        public virtual Servicio Servicio { get; set; } = null!;
        public virtual HistorialServicio? HistorialServicio { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
