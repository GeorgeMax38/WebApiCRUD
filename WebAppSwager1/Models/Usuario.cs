using System.ComponentModel.DataAnnotations;

namespace WebAppSwager1.Models
{
    public class Usuario
    {
        //[Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? CorreoElectronico { get; set; }
        public DateTime? FechaDeAlta { get; set; }
        public bool? Activo { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
    }
}
