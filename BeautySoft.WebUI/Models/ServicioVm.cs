namespace BeautySoft.WebUI.Models
{
    public class ServicioVm
    {
        public int ServicioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int DuracionMinutos { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; } = true;
    }
}
