using CaesarApi.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaesarApi.Repositories
{
    public interface IMensajeRepository
    {
        Task<Mensaje> AddMensajeAsync(Mensaje mensaje);
        Task<List<Mensaje>> GetMensajesAsync();
    }
} 