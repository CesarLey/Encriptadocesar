using CaesarApi.Entities.Models;
using CaesarApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CaesarApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MensajesController : ControllerBase
    {
        private readonly IMensajeService _mensajeService;
        public MensajesController(IMensajeService mensajeService)
        {
            _mensajeService = mensajeService;
        }

        [HttpPost("encriptar")]
        [AllowAnonymous]
        public async Task<IActionResult> Encriptar([FromBody] string mensaje)
        {
            if (string.IsNullOrWhiteSpace(mensaje))
                return BadRequest("El mensaje no puede estar vacío.");
            var cifrado = await _mensajeService.EncriptarYGuardarAsync(mensaje);
            return Ok(new { mensajeEncriptado = cifrado });
        }

        [HttpPost("desencriptar")]
        [AllowAnonymous]
        public async Task<IActionResult> Desencriptar([FromBody] string mensajeEncriptado)
        {
            if (string.IsNullOrWhiteSpace(mensajeEncriptado))
                return BadRequest("El mensaje encriptado no puede estar vacío.");
            var original = await _mensajeService.DesencriptarAsync(mensajeEncriptado);
            return Ok(new { mensajeOriginal = original });
        }

        [HttpGet("mensajes")]
        public async Task<IActionResult> GetMensajes()
        {
            var mensajes = await _mensajeService.ObtenerMensajesAsync();
            return Ok(mensajes);
        }
    }
} 