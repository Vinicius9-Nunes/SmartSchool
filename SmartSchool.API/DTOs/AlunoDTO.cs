using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.DTOs
{
    public class AlunoDTO
    {
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public int Idade { get; set; }
        public DateTime DataInicio { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
