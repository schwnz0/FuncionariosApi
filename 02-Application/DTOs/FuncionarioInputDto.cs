using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Application.DTOs
{
    public class FuncionarioInputDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O cargo é obrigatório.")]
        public string Cargo { get; set; } = string.Empty;

        [Range(typeof(decimal), "0.01", "999999999.99", ErrorMessage = "O salário deve ser maior que zero.")]
        public decimal Salario { get; set; }

        [Required(ErrorMessage = "O departamento é obrigatório.")]
        public string Departamento { get; set; } = string.Empty;
    }
}
