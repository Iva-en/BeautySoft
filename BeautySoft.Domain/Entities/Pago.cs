using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySoft.Domain.Entities
{
    public class Pago
    {
        public int PagoId { get; set; }
        public int CitaId { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public DateTime FechaPago { get; set; } = DateTime.Now;

        public virtual Cita Cita { get; set; } = null!;
    }
}
