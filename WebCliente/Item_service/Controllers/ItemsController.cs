using Microsoft.AspNetCore.Mvc;
using Item_service.Models;
using Item_service.Repositories;
using Item_service.Services;

namespace Item_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly ServicioDistribucion _servicio;

        public ItemsController(ServicioDistribucion servicio)
        {
            _servicio = servicio;
        }

        // Crear y asignar un nuevo ítem
        [HttpPost("asignar")]
        public async Task<IActionResult> Asignar(Item item)
        {
            var usuario = await _servicio.AsignarItem(item);
            if (usuario == null) return BadRequest("No hay usuarios disponibles");

            return Ok(new { Usuario = usuario.NombreUsuario, Item = item.Titulo });
        }

        // Obtener todos los ítems pendientes
        [HttpGet("pendientes")]
        public IActionResult ObtenerPendientes()
        {
            return Ok(ItemRepositorio.ObtenerPendientes());
        }

        // Marcar un ítem como completado
        [HttpPost("completar/{id}")]
        public IActionResult Completar(int id)
        {
            ItemRepositorio.MarcarCompletado(id);
            return Ok(new { Mensaje = $"El ítem {id} fue marcado como completado." });
        }
    }
}
