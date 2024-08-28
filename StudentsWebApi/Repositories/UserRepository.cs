using System.Collections.Generic;
using StudentsWebApi.Models;

namespace StudentsWebApi.Repositories
{
    public static class UserRepository
    {
        public static UserModel Get(string nomeusuario, string senha)
        {
            var user = new List<UserModel>
            {
                new UserModel {
                Id = 1,
                NomeUsuario = "admin",
                Senha = "admin"
                }
            };

            return (UserModel)user.FirstOrDefault(x => x.NomeUsuario.ToLower() == nomeusuario && x.Senha == senha);
        }
    }
}
