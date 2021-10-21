using com.NascimentoSoftware.BookStore.Infraestrutura.Infra.E_commerce.Processos;
using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Repositorios.Repository;
using com.NascimentoSoftware.BookStore.WebApp.Models;
using com.NascimentoSoftware.BookStore.WebApp.Models.eCommerce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.WebApp.Controllers
{
    public class CarrinhoController : Controller
    {
        [Authorize]
        public async Task<IActionResult> Index([FromServices] AdicionarProdutoCarrinho produtoCarrinho, [FromServices] LivroRepository livroRepo)
        {
            var lista = await produtoCarrinho.GetProdutosCarrinho(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<ProdutoCarrinho> produtoCarrinhos = new List<ProdutoCarrinho>();
            foreach (var item in lista)
            {
                var livro = await livroRepo.GetOne(item.ProdutoId);
                produtoCarrinhos.Add(new ProdutoCarrinho
                {
                    IdCarrinho = item.CarrinhoId,
                    IdProduto = livro.Id,
                    NomeProduto = livro.Nome,
                    PrecoProduto = "39"
                });
            }
           

            return View(produtoCarrinhos);
        }
        [Authorize]
        public async Task<IActionResult> RemoverCarrinho([FromServices] AdicionarProdutoCarrinho produtoCarrinho, int id)
        {
            try
            {//ToDo: falha conhecida: se existir dois produtos idênticos no carrinho, ao clicar em remover ele deleta os dois. Isso ocorre pelo fato do delete ser feito pelo ID DO PRODUTO e não por um id único
            //-chave primária- da tabela Produto_Carrinho
                var carrinho = await produtoCarrinho.GetCarrinho(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await produtoCarrinho.RemoverProduto(carrinho.Id, id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize]
        public async Task<IActionResult> FinalizarCompra([FromServices] AdicionarProdutoCarrinho produtoCarrinho, [FromServices] LivroRepository livroRepo)
        {
            var listaProdutosCarrinho = await produtoCarrinho.GetProdutosCarrinho(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var listaModel = new List<Livro>();

            foreach(var item in listaProdutosCarrinho)
            {
                var livro = await livroRepo.GetOne((int)item.ProdutoId);
                listaModel.Add(new Livro
                {
                    Id = livro.Id,
                    Nome = livro.Nome,
                    CategoriaId = livro.CategoriaId,
                    DataAtualizacao = livro.DataAtualizacao,
                    DataRegistro = livro.DataRegistro,
                });
            }

            var produto = new ProdutosFinalizacao();
            produto.listaLivros = listaModel;
            produto.Valor_Total = 50;
            produto.Valor_Pago = 0;


            return View(produto);
        }

    }
}
