using WebAppSwager1.Models;

namespace WebAppSwager1.Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<Usuario> Registrar(Usuario usuario, string password);
        Task<Usuario> Login(string CorreoElectronico, string password);
        Task<bool> ExisteUsuario(string correoElectronico);
    }
}
