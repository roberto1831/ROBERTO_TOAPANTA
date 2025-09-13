namespace ROBERTO_TOPANTA.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public int TotalItemsAsignados { get; set; }
        public int ItemsAltaPrioridad { get; set; }
    }
}
