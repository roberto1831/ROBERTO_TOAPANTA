namespace Item_service.Models
{
    public enum Prioridad
    {
        Baja,
        Alta
    }

    public class Item
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string UsuarioAsignado { get; set; } = string.Empty;
        public DateTime FechaEntrega { get; set; } = DateTime.Now.AddDays(7);
        public bool Completado { get; set; } = false;
        public Prioridad Relevancia { get; set; } = Prioridad.Baja; 
    }
}
