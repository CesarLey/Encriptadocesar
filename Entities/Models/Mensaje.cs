using System;

namespace CaesarApi.Entities.Models
{
    public class Mensaje
    {
        public int Id { get; set; }
        public string MensajeOriginal { get; set; }
        public string MensajeEncriptado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
} 