namespace Usuario_service.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public int TotalItemsAsignados { get; set; } = 0;
        public int ItemsAltaPrioridad { get; set; } = 0;
    }
}
