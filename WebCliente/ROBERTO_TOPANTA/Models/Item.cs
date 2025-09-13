namespace ROBERTO_TOPANTA.Models
{
    public enum Prioridad { Baja, Alta }

    public class Item
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public DateTime FechaEntrega { get; set; }
        public Prioridad Relevancia { get; set; }
        public string UsuarioAsignado { get; set; } = string.Empty;
        public bool EstaCompletado { get; set; }
    }
}
