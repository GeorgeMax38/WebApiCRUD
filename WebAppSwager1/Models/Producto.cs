using System.ComponentModel.DataAnnotations;

namespace WebAppSwager1.Models
{
    public class Producto
    {
        //[Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaDeAlta { get; set; }
        public System.Decimal? Precio { get; set; }
        public bool? Activo { get; set; }
    }
}
