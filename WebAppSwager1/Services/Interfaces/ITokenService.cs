using WebAppSwager1.Models;

namespace WebAppSwager1.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Usuario usuario);
    }
}
