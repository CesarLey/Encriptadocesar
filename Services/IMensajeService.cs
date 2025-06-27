using CaesarApi.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaesarApi.Services
{
    public interface IMensajeService
    {
        Task<string> EncriptarYGuardarAsync(string mensaje);
        Task<string> DesencriptarAsync(string mensajeEncriptado);
        Task<List<Mensaje>> ObtenerMensajesAsync();
    }
} 