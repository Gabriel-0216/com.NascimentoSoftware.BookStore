using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Repositorios.Repository;
using com.NascimentoSoftware.BookStore.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.WebApp.Controllers
{
    public class CategoriaController : Controller
    {
        public async Task<IActionResult> Index([FromServices] CategoriaRepository repository)
        {
            var listaCategoria = new List<Categoria>();
            if (await repository.GetAll() == null)
            {
                return NotFound();
            }
            else
            {
                foreach (var item in await repository.GetAll())
                {
                    listaCategoria.Add(new Categoria
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        DataRegistro = item.DataRegistro,
                        DataAtualizacao = item.DataAtualizacao,
                    });
                }
                return View(listaCategoria);
            }
        }
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromServices] CategoriaRepository repository, Categoria model)
        {
            if (ModelState.IsValid)
            {
                var categoriaInfra = new Infraestrutura.Infraestrutura.Models.Cadastro.Categoria()
                {
                    Nome = model.Nome,
                    DataAtualizacao = DateTime.Now,
                    DataRegistro = DateTime.Now,
                };
                await repository.Add(categoriaInfra);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit([FromServices] CategoriaRepository repository, int? id)
        {
            var categoriaInfra = await repository.GetOne((int)id);
            if (id == null || categoriaInfra == null)
            {
                return NotFound();
            }
            else
            {
                var categoriaModel = new Categoria()
                {
                    Id = categoriaInfra.Id,
                    Nome = categoriaInfra.Nome,
                    DataAtualizacao = categoriaInfra.DataAtualizacao,
                    DataRegistro = categoriaInfra.DataRegistro,
                };
                return View(categoriaModel);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit([FromServices] CategoriaRepository repository, Categoria model)
        {
            if (ModelState.IsValid)
            {
                var categoriaInfra = new Infraestrutura.Infraestrutura.Models.Cadastro.Categoria()
                {
                    Id = model.Id,
                    DataAtualizacao = model.DataAtualizacao,
                    DataRegistro = model.DataRegistro,
                    Nome = model.Nome,
                };
                await repository.Update(categoriaInfra);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete([FromServices] CategoriaRepository repository, int? id)
        {
            var categoriaInfra = await repository.GetOne((int)id);
            if(id==null || categoriaInfra == null)
            {
                return NotFound();
            }
            else
            {
                var categoriaModel = new Categoria()
                {
                    Nome = categoriaInfra.Nome,
                    Id = categoriaInfra.Id,
                    DataAtualizacao = categoriaInfra.DataAtualizacao,
                    DataRegistro = categoriaInfra.DataRegistro,
                };
                return View(categoriaModel);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete([FromServices] CategoriaRepository repository, int id)
        {
            try
            {
                await repository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details([FromServices] CategoriaRepository repository, int? id)
        {
            if (id == null || await repository.GetOne((int)id) == null)
            {
                return NotFound();
            }
            else
            {
                var categoriaInfra = await repository.GetOne((int)id);
                var categoriaModel = new Categoria()
                {
                    Id = categoriaInfra.Id,
                    Nome = categoriaInfra.Nome,
                    DataRegistro = categoriaInfra.DataRegistro,
                    DataAtualizacao = categoriaInfra.DataAtualizacao,
                };
                return View(categoriaModel);
            }
        }
    }
}