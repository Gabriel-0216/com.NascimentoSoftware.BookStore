using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.WebApp.Models.ViewModel
{
    public class LivroViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataRegistro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int CategoriaId { get; set; }
        public IEnumerable<Categoria> categorias { get; set; }

        public IEnumerable<Autor> autores { get; set; }
        public int AutorId { get; set; }


    }
}
