using Microsoft.AspNetCore.Mvc.RazorPages;
using ROBERTO_TOPANTA.Models;
using System.Net.Http.Json;

namespace ROBERTO_TOPANTA.Pages
{
    public class UsuariosModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public UsuariosModel(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        public List<Usuario> ListaUsuarios { get; set; } = new();

        public async Task OnGet()
        {
            var client = _clientFactory.CreateClient();

            // 🔹 Obtiene la URL desde appsettings.json
            var baseUrl = _config["ServiceUrls:UsuarioService"];
            client.BaseAddress = new Uri(baseUrl);

            var result = await client.GetFromJsonAsync<List<Usuario>>("api/usuarios");
            if (result != null)
                ListaUsuarios = result;
        }
    }
}
