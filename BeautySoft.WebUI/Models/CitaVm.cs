using System;
using System.ComponentModel.DataAnnotations;

namespace BeautySoft.WebUI.Models
{
    public class CitaVm
    {
        public int CitaId { get; set; }

        [Display(Name = "Cliente")]
        [Required]
        public int ClienteId { get; set; }

        [Display(Name = "Estilista")]
        [Required]
        public int EstilistaId { get; set; }

        [Display(Name = "Servicio")]
        [Required]
        public int ServicioId { get; set; }

        [Display(Name = "Fecha y hora")]
        [Required]
        public DateTime FechaHora { get; set; }

        [Display(Name = "Estado")]
        [Required]
        public string Estado { get; set; } = "Programada";

        [Display(Name = "Precio final")]
        public decimal? PrecioFinal { get; set; }

        [Display(Name = "Notas")]
        [StringLength(255)]
        public string? Notas { get; set; }
    }
}
