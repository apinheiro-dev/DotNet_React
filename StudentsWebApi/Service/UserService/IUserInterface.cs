using Microsoft.AspNetCore.Mvc;
using StudentsWebApi.Models;

namespace StudentsWebApi.Service.UserService
{
    public interface IUserInterface
    {
        Task<ServiceResponse<UserModel>> AutenticarUsuario(string login, string senha);

        Task<IActionResult> LogarUsuarioAsync([FromHeader] string login, [FromHeader] string senha);

        object GenerateToken(UserModel user);

    }
}
