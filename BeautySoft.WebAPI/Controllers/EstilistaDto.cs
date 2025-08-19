namespace BeautySoft.WebAPI.Models
{
    public class EstilistaDto
    {
        public int EstilistaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public decimal PorcentajeComision { get; set; } = 30.00m;
        public bool Activo { get; set; } = true;
    }
}
