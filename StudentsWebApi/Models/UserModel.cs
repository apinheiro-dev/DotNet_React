using System.ComponentModel.DataAnnotations;

namespace StudentsWebApi.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }

    }
}
