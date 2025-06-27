using CaesarApi.Entities.Models;
using CaesarApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaesarApi.Services
{
    public class MensajeService : IMensajeService
    {
        private readonly IMensajeRepository _repo;
        private readonly ICifradoService _cifradoService;
        public MensajeService(IMensajeRepository repo, ICifradoService cifradoService)
        {
            _repo = repo;
            _cifradoService = cifradoService;
        }

        public async Task<string> EncriptarYGuardarAsync(string mensaje)
        {
            if (string.IsNullOrWhiteSpace(mensaje))
                throw new System.ArgumentException("El mensaje no puede estar vacío.");
            var cifrado = _cifradoService.Cifrar(mensaje);
            var entidad = new Mensaje { MensajeEncriptado = cifrado };
            await _repo.AddMensajeAsync(entidad);
            return cifrado;
        }

        public async Task<string> DesencriptarAsync(string mensajeEncriptado)
        {
            if (string.IsNullOrWhiteSpace(mensajeEncriptado))
                throw new System.ArgumentException("El mensaje encriptado no puede estar vacío.");
            return _cifradoService.Descifrar(mensajeEncriptado);
        }

        public async Task<List<Mensaje>> ObtenerMensajesAsync()
        {
            return await _repo.GetMensajesAsync();
        }
    }
} 