using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySoft.Domain.Entities
{
    public class Comision
    {
        public int ComisionId { get; set; }
        public int EstilistaId { get; set; }
        public int CitaId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaGeneracion { get; set; } = DateTime.Now;
        public bool Liquidada { get; set; } = false;

        public virtual Estilista Estilista { get; set; } = null!;
        public virtual Cita Cita { get; set; } = null!;
    }
}
