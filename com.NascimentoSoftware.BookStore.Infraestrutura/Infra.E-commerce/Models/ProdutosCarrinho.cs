using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Models.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.Infraestrutura.Infra.E_commerce.Models
{
    public class ProdutosCarrinho
    {
        public int id { get; set; }
        public int CarrinhoId { get; set; }
        public int ProdutoId { get; set; }
        public decimal ValorProduto { get; set; }


    }
}
