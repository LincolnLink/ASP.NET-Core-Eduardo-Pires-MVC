using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaDemoMvc.Models
{
    public class testeScaffold
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Título é obrigatório")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O maximo de caracteres é 30 e o minimo é 3")]
        public string Nome { get; set; }
    }
}
