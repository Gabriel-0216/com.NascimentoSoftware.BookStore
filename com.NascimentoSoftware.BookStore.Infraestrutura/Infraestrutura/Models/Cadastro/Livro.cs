using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Models.Cadastro
{
   public class Livro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataRegistro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int CategoriaId { get; set; }

    }
}
