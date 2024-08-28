using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentsWebApi.DataContext;
using StudentsWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentsWebApi.Service.UserService
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public static string GerarToken(UserModel user)
        {
            var tokenHander = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(ConfigurationBuilder["jwt:secretKey"]);
            var key = Encoding.ASCII.GetBytes(WebApplication.CreateBuilder().Configuration["jwt:secretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.NomeUsuario.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHander.CreateToken(tokenDescriptor);
            return tokenHander.WriteToken(token);
        }

        //public async Task<IActionResult> LogarUsuarioAsync([FromQuery] string login, [FromBody] string senha)
        public async Task<IActionResult> LogarUsuarioAsync([FromHeader] string login, [FromHeader] string senha)
        {
            var serviceResponse1 = new ServiceResponse<List<StudentModel>>();

            ServiceResponse<UserModel> serviceResponse = new ServiceResponse<UserModel>();

            try
            {
                // StudentModel student = _context.Students.FirstOrDefault(x => x.Id == id);
                UserModel user = _context.Users.FirstOrDefault(x => x.NomeUsuario == login && x.Senha == senha);

                if (user != null)
                {
                    //object token = UserService.GenerateToken(new UserModel());
                    object token = GenerateToken(new UserModel());
                    
                    serviceResponse.Dados = user;

                    //return (ActionResult)token;
                    return (ActionResult)token;
                }

                //if (user == null)
                //{
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário e/ou senha inválido(s).";
                    serviceResponse.Sucesso = false;
                //}

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            //return BadRequestResult("Usuário e/ou senha inválido(s).");
            // return badRequestResult;


            return (IActionResult)serviceResponse;
        }

        public static object GenerateToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(WebApplication.CreateBuilder().Configuration["jwt:secretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                 {
                     //new Claim(ClaimTypes.Name, user.NomeUsuario.ToString())
                     new Claim("idUsuario", user.Id.ToString())
                 }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            //Gerando token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //Retornando como string
            return tokenHandler.WriteToken(token);
        }
    }
}
