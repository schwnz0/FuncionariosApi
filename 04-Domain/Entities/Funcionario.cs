using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Domain.Entities
{
    public class Funcionario
    {
        public int Id { get; set; }
        [Required] public string Nome { get; set; }
        [Required] public string Cargo { get; set; }
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "O salário deve ser um valor positivo.")]
        public decimal Salario { get; set; }
        [Required] public string Departamento { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
