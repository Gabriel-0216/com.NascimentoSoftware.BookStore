using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.WebApp.Models
{
    public class Livro
    {
        public int Id { get; set; }

        [Required(ErrorMessage="É necessário digitar o nome do livro: ")]
        public string Nome { get; set; }
        public DateTime DataRegistro { get; set; }
        public DateTime DataAtualizacao { get; set; }

        [Required(ErrorMessage = "É necessário informar a categoria do livro: ")]
        [Display(Name ="Categoria")]
        public int CategoriaId { get; set; }

        public List<Categoria> categorias { get; set; }

    }
}
