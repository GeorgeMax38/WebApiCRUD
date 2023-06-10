namespace WebAppSwager1.Dtos
{
    public class DtoUserRegister
    {
        public string? CorreoElectronico { get; set; }
        public string? Password { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaDeAlta { get; set; }
        public bool? Activo { get; set; }
        public DtoUserRegister()
        {
            FechaDeAlta = DateTime.Now;
            Activo = true;
        }
    }
}
