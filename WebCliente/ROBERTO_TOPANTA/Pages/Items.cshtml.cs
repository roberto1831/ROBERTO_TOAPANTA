using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ROBERTO_TOPANTA.Models;
using System.Net.Http.Json;

namespace ROBERTO_TOPANTA.Pages
{
    public class ItemsModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public ItemsModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public List<Item> ItemsPendientes { get; set; } = new();

        [BindProperty]
        public Item NuevoItem { get; set; } = new();

        // GET: obtener ítems pendientes
        public async Task OnGet()
        {
            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7058"); // Item_service

            var result = await client.GetFromJsonAsync<List<Item>>("/api/items/pendientes");
            if (result != null)
                ItemsPendientes = result;
        }

        // POST: crear un ítem nuevo y asignarlo automáticamente
        public async Task<IActionResult> OnPostCrearAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7058");

            await client.PostAsJsonAsync("/api/items/asignar", NuevoItem);

            return RedirectToPage("/Items");
        }

        // POST: marcar un ítem como completado
        public async Task<IActionResult> OnPostCompletarAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7058");

            await client.PostAsync($"/api/items/completar/{id}", null);

            return RedirectToPage("/Items");
        }
    }
}
