using System.Net.Http.Json;
using Item_service.Models;
using Item_service.Repositories;

namespace Item_service.Services
{
    public class ServicioDistribucion
    {
        private readonly HttpClient _httpClient;

        public ServicioDistribucion(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;

            var url = config["ServiceUrls:UsuarioService"];
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("ServiceUrls:UsuarioService",
                    "La URL del servicio de usuarios no está configurada en appsettings.json");
            }

            _httpClient.BaseAddress = new Uri(url);
        }

        public async Task<UsuarioAsignado?> AsignarItem(Item item)
        {
            var usuarios = await _httpClient.GetFromJsonAsync<List<UsuarioAsignado>>("api/usuarios");
            if (usuarios == null || usuarios.Count == 0) return null;

            // Condición por fecha (entregas en menos de 3 días)
            if ((item.FechaEntrega - DateTime.Now).TotalDays < 3)
            {
                var user = usuarios.OrderBy(u => u.ItemsAsignados).FirstOrDefault();
                if (user != null)
                {
                    item.UsuarioAsignado = user.NombreUsuario ?? "SinNombre";
                    ItemRepositorio.Agregar(item);
                    return user;
                }
            }

            // Condición por prioridad Alta
            if (item.Relevancia == Prioridad.Alta)
            {
                var user = usuarios
                    .Where(u => u.ItemsAltaPrioridad < 3)
                    .OrderBy(u => u.ItemsAsignados)
                    .FirstOrDefault();

                if (user != null)
                {
                    item.UsuarioAsignado = user.NombreUsuario ?? "SinNombre";
                    ItemRepositorio.Agregar(item);
                    return user;
                }
            }

            // Fallback: asignar al que menos items tiene
            var fallback = usuarios.OrderBy(u => u.ItemsAsignados).FirstOrDefault();
            if (fallback != null)
            {
                item.UsuarioAsignado = fallback.NombreUsuario ?? "SinNombre";
                ItemRepositorio.Agregar(item);
                return fallback;
            }

            return null;
        }
    }

    public class UsuarioAsignado
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public int ItemsAsignados { get; set; }
        public int ItemsAltaPrioridad { get; set; }
    }
}
