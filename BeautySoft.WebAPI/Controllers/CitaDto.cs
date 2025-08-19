using System;

namespace BeautySoft.WebAPI.Models
{
    public class CitaDto
    {
        public int CitaId { get; set; }
        public int ClienteId { get; set; }
        public int EstilistaId { get; set; }
        public int ServicioId { get; set; }
        public DateTime FechaHora { get; set; }
        public string Estado { get; set; } = "Programada"; // Programada, Completada, Cancelada, NoShow
        public decimal? PrecioFinal { get; set; }
        public string? Notas { get; set; }
    }
}
