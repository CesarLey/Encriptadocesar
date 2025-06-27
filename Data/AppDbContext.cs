using CaesarApi.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace CaesarApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Mensaje> Mensajes { get; set; }
    }
} 