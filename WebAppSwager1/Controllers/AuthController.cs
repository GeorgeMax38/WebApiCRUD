using Microsoft.AspNetCore.Mvc;
using WebAppSwager1.Data.Interfaces;
using WebAppSwager1.Dtos;
using WebAppSwager1.Models;
using WebAppSwager1.Services.Interfaces;

namespace WebAppSwager1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        private readonly ITokenService _tokenService;
        public AuthController(IAuthRepository repo, ITokenService tokenService)
        {
            _repo = repo;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(DtoUserRegister DtoUser)
        {
            DtoUser.CorreoElectronico = DtoUser.CorreoElectronico.ToLower();

            if (await _repo.ExisteUsuario(DtoUser.CorreoElectronico))
                return BadRequest("Usuario ya registrado");

            var UserNew = new Usuario();
            UserNew.Nombre = DtoUser.Nombre;
            UserNew.CorreoElectronico = DtoUser.CorreoElectronico;
            UserNew.FechaDeAlta = DtoUser.FechaDeAlta;
            UserNew.Activo = DtoUser.Activo;
            var UsuarioCreado = await _repo.Registrar(UserNew, DtoUser.Password);

            var UserNewDto = new Usuario();
            UserNewDto.Id = UsuarioCreado.Id;
            UserNewDto.Nombre = UsuarioCreado.Nombre;
            UserNewDto.CorreoElectronico = UsuarioCreado?.CorreoElectronico;
            UserNewDto.FechaDeAlta = UsuarioCreado?.FechaDeAlta;
            UserNewDto.Activo = UsuarioCreado?.Activo;
            return Ok(UserNewDto);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(DtoUsuarioLogin dtoUsuario)
        {
            var userFromRepo = await _repo.Login(dtoUsuario.CorreoElectronico, dtoUsuario.Password);
            
            if (userFromRepo == null)
                return Unauthorized();

            var usuario = new Usuario();
            usuario.Id = userFromRepo.Id;
            usuario.Nombre = userFromRepo.Nombre;
            usuario.CorreoElectronico = userFromRepo?.CorreoElectronico;
            usuario.FechaDeAlta = userFromRepo?.FechaDeAlta;
            usuario.Activo = userFromRepo?.Activo;

            var token = _tokenService.CreateToken(usuario);

            return Ok(new
            {
                token = token,
                usuario = usuario,
            });
        }
    }
}
