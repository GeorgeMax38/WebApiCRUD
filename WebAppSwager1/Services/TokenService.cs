using Microsoft.IdentityModel.Tokens;
using WebAppSwager1.Models;
using WebAppSwager1.Services.Interfaces;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace WebAppSwager1.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _sskey;
        public TokenService(IConfiguration config)
        {
            _sskey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetConnectionString("KeyToken")));
            //string passw = config.GetConnectionString("KeyToken");
            //_sskey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(passw));
        }
        public string CreateToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.CorreoElectronico)
            };

            var credenciales = new SigningCredentials(_sskey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credenciales
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
