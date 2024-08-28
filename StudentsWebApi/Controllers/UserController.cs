using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsWebApi.Models;
using StudentsWebApi.Service.UserService;
//using SecureIdentity.Password;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StudentsWebApi.DataContext;
using Microsoft.EntityFrameworkCore;
using StudentsWebApi.Service.StudentService;
//using Microsoft.AspNet.Identity;

namespace StudentsWebApi.Controllers
{
    //[Route("api/[controller]")]
    //[Authorize]
    //[Route("api/auth")]
    //[Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;

        public UserController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        /// <summary>
        ///     Login - Gerador Token
        /// </summary>
        /// <returns>Token de acesso</returns>
        /// <response code="200">Sucess</response>
        [HttpPost("/api/login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginAsync([FromBody] UserModel model)
        {

            if (model.NomeUsuario == "admin" && model.Senha == "admin")
            {
                //object token = UserService.GenerateToken(new UserModel());
                object token = _userInterface.GenerateToken(new UserModel());
                return Ok(token);
            }



            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState.Values);

            ////var user = await context
            //var user = await context
            //    .Users
            //    //.Include(x => x.Roles)
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(x => x.NomeUsuario == model.NomeUsuario);

            //if (user == null)
            //    return StatusCode(401, "Login ou Senha inválido");

            ////if (!PasswordHasher.Verify(user.PasswordHash, model.Senha))
            //if (!UserManager.PasswordHasher.VerifyHashedPassword(model.Senha))
            //    return StatusCode(401, "Login e/ou Senha inválido(s)");

            //try
            //{
            //    var token = userService.GenerateToken(user);
            //    return Ok(token);
            //}
            //catch
            //{
            //    return StatusCode(500, "Internal Error");
            //}

            return BadRequest("Usuário e/ou senha inválido(s).");
        }

        /// <summary>
        ///     Login
        /// </summary>
        /// <returns>Token de acesso</returns>
        /// <response code="200">Sucess</response>
        [HttpPost("/api/auth/loginAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LogarUsuarioAsync([FromHeader] string login, [FromHeader] string senha)
        {
            return Ok(await _userInterface.LogarUsuarioAsync(login, senha));
        }

    }
}
