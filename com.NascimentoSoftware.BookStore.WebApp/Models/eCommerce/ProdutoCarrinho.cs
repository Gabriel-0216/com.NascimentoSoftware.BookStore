using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.WebApp.Models.eCommerce
{
    public class ProdutoCarrinho
    {
        public int IdCarrinho { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public string PrecoProduto { get; set; }

    }
}
