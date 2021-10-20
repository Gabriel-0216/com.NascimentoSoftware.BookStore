using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Repositorios.Repository;
using com.NascimentoSoftware.BookStore.WebApp.Models;
using com.NascimentoSoftware.BookStore.WebApp.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.WebApp.Controllers
{
    public class LivroController : Controller
    {
        public async Task<IActionResult> Index([FromServices] LivroRepository repository)
        {
            var listaLivros = new List<Livro>();
            foreach(var item in await repository.GetAll())
            {
                listaLivros.Add(new Livro()
                {
                    Id = item.Id,
                    CategoriaId = item.CategoriaId,
                    Nome = item.Nome,
                    DataAtualizacao = item.DataAtualizacao,
                    DataRegistro = item.DataRegistro,
                });
            }
 
            return View(listaLivros);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create([FromServices] CategoriaRepository categoriaRepository, [FromServices] AutorRepository autorRepository) // CADASTRAR LIVRO AUTOR//
        {
            var viewModel = new LivroViewModel();
            viewModel.categorias = await PreencherListaCategoria(categoriaRepository);
            viewModel.autores = await PreencherListaAutores(autorRepository);
            return View(viewModel);
        } 

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromServices] RegistroLivroRepository repository, [FromServices] AutorRepository autorRepo, LivroViewModel model) //Add LivroAutor
        {
            if (ModelState.IsValid)
            {
                var livroInfra = new Infraestrutura.Infraestrutura.Models.Cadastro.Livro()
                {
                    Nome = model.Nome,
                    DataAtualizacao = DateTime.Now,
                    DataRegistro = DateTime.Now,
                    CategoriaId = model.CategoriaId,
                };
                var autorInfra = await autorRepo.GetOne((int)model.AutorId);
                await repository.InserirLivro(livroInfra, autorInfra);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete([FromServices] LivroRepository repository, int? id) // delete LivroAutor
        {
            if(id==null || await repository.GetOne((int)id) == null)
            {
                return NotFound();
            }
            else
            {
                var livroInfra = await repository.GetOne((int)id);
                var livroModel = new Livro()
                {
                    Nome = livroInfra.Nome,
                    CategoriaId = livroInfra.CategoriaId,
                    DataAtualizacao = livroInfra.DataAtualizacao,
                    DataRegistro = livroInfra.DataRegistro,
                    Id = livroInfra.Id,
                };
                return View(livroModel);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete([FromServices] RegistroLivroRepository repository, int id)
        {
            try
            {
                await repository.DeletarLivro((int)id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        private async Task<IEnumerable<Autor>> PreencherListaAutores(AutorRepository repository)
        {
            var listaAutores = new List<Autor>();
            foreach (var item in await repository.GetAll())
            {
                listaAutores.Add(new Autor()
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Sobrenome = item.Sobrenome,
                    DataAtualizacao = item.DataAtualizacao,
                    DataRegistro = item.DataRegistro,
                });
            }
            return listaAutores;
        }
        private async Task<IEnumerable<Categoria>> PreencherListaCategoria(CategoriaRepository repository)
        {
            var listaCategorias = new List<Categoria>();
            foreach (var item in await repository.GetAll())
            {
                listaCategorias.Add(new Categoria()
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    DataAtualizacao = item.DataAtualizacao,
                    DataRegistro = item.DataRegistro,
                });
            }
            return listaCategorias;
        }
    }
}
