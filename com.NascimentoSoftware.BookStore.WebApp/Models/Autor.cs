using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.WebApp.Models
{
    public class Autor
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="É necessário definir o nome do autor.")]
        [StringLength(255, ErrorMessage ="O número máximo de caracteres é 255 e o mínimo é 5", MinimumLength = 5)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É necessário definir o sobrenome do autor.")]
        [StringLength(255, ErrorMessage = "O número máximo de caracteres é 255 e o mínimo é 5", MinimumLength = 5)]
        public string Sobrenome { get; set; }

        [Display(Name = "Data de registro")]
        public DateTime DataRegistro { get; set; }

        [Display(Name = "Data de Atualização do registro")]
        public DateTime DataAtualizacao { get; set; }
    }
}
