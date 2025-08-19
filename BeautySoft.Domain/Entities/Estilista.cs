using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySoft.Domain.Entities
{
    public class Estilista
    {
        public int EstilistaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public decimal PorcentajeComision { get; set; } = 30.00m;
        public bool Activo { get; set; } = true;

        public virtual ICollection<Cita> Citas { get; set; } = new List<Cita>();
        public virtual ICollection<Comision> Comisiones { get; set; } = new List<Comision>();
    }
}
