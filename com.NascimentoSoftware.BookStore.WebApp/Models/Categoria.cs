using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.WebApp.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "É necessário informar o nome da categoria")]
        public string Nome { get; set; }
        public DateTime DataRegistro { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
