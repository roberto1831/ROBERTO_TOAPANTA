using Item_service.Models;

namespace Item_service.Repositories
{
    public static class ItemRepositorio
    {
        private static List<Item> _items = new();

        public static void Agregar(Item item) => _items.Add(item);

        public static List<Item> ObtenerTodos() =>
            _items.OrderBy(i => i.FechaEntrega).ToList();

        public static List<Item> ObtenerPendientes() =>
            _items.Where(i => !i.Completado)
                  .OrderBy(i => i.FechaEntrega)
                  .ToList();

        public static void MarcarCompletado(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            if (item != null) item.Completado = true;
        }
    }
}
