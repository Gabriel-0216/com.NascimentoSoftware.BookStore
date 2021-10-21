using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.WebApp.Models.eCommerce
{
    public class ProdutosFinalizacao
    {
        public int IdUsuario { get; set; }
        public List<Livro> listaLivros { get; set; }
        public decimal Valor_Total { get; set; }
        public decimal Valor_Pago { get; set; }

    }
}
