﻿using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace StudentsWebApi.Models
{
    public class StudentModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public int Serie { get; set; }
        public double NotaMedia { get; set; }
        public string Endereco { get; set; }
        public string NomePai { get; set; }
        public string NomeMae { get; set; }
        public DateTime DataNascimento { get; set; }


    }
}
