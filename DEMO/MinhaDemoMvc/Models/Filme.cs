using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaDemoMvc.Models
{
    public class Filme
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "O campo Título é obrigatório")]
        [StringLength(30,MinimumLength = 3, ErrorMessage = "O maximo de caracteres é 30 e o minimo é 3")]
        public string Titulo { get; set; }


        [Required(ErrorMessage = "O campo Data de Lançamento é obrigatório")]
        [DataType(DataType.DateTime, ErrorMessageResourceName = "Data em formato incorreto")]
        [Display(Name = "Data de Lançamento")]
        public DateTime DataLancamento { get; set; }


        [RegularExpression(@"^[A-Z]+[a-zA-Z\u00C0-\u00FF""'\w-]*$",
        ErrorMessage = "Genero formato errado")]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres"),
        Required(ErrorMessage = "O campo Título é obrigatório")]
        public string Genero { get; set; }


        [Required(ErrorMessage = "O campo valor é obrigatório")]
        [Range(1,1000, ErrorMessage ="Minimo 1 a 1000")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo Avaliação é obrigatório")]
        [Display(Name = "Avaliação")]
        [RegularExpression(@"^[0-5]*$", ErrorMessage ="Somente números de 0 á 5")]
        public int Avalicao { get; set; }


    }
}
