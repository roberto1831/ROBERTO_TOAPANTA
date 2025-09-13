using Microsoft.AspNetCore.Mvc;
using Usuario_service.Models;

namespace Usuario_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private static List<Usuario> _usuarios = new()
        {
            new Usuario { Id = 1, NombreUsuario = "Juan Diaz", TotalItemsAsignados = 3, ItemsAltaPrioridad = 1 },
            new Usuario { Id = 2, NombreUsuario = "Maria Toques", TotalItemsAsignados = 5, ItemsAltaPrioridad = 2 },
            new Usuario { Id = 3, NombreUsuario = "Pedro Estevez", TotalItemsAsignados = 2, ItemsAltaPrioridad = 0 }
        };

        private static int _ultimoUsuario = -1;

        [HttpGet]
        public IActionResult ObtenerUsuarios() => Ok(_usuarios);

        [HttpGet("{nombreUsuario}")]
        public IActionResult ObtenerUsuario(string nombreUsuario)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        
        [HttpGet("asignar")]
        public IActionResult AsignarUsuario()
        {
            if (!_usuarios.Any()) return NotFound("No hay usuarios disponibles");

            _ultimoUsuario = (_ultimoUsuario + 1) % _usuarios.Count;
            var usuario = _usuarios[_ultimoUsuario];

            return Ok(new { NombreUsuario = usuario.NombreUsuario });
        }
    }
}
