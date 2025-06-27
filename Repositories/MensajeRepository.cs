using CaesarApi.Data;
using CaesarApi.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaesarApi.Repositories
{
    public class MensajeRepository : IMensajeRepository
    {
        private readonly AppDbContext _context;
        public MensajeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Mensaje> AddMensajeAsync(Mensaje mensaje)
        {
            _context.Mensajes.Add(mensaje);
            await _context.SaveChangesAsync();
            return mensaje;
        }

        public async Task<List<Mensaje>> GetMensajesAsync()
        {
            return await _context.Mensajes.ToListAsync();
        }
    }
} 