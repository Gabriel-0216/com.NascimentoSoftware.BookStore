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
    public class AutorController : Controller
    {
        public async Task<IActionResult> Index([FromServices] AutorRepository repository)
        {
            try
            {
                var listaAutores = new List<Autor>();
                foreach (var item in await repository.GetAll())
                {
                    listaAutores.Add(new Autor
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        Sobrenome = item.Sobrenome,
                        DataAtualizacao = item.DataAtualizacao,
                        DataRegistro = item.DataRegistro,
                    });
                }
                return View(listaAutores);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Create([FromServices] AutorRepository repository, Autor model)
        {
            if (ModelState.IsValid)
            {
                var autor = new Infraestrutura.Infraestrutura.Models.Cadastro.Autor()
                {
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    DataRegistro = DateTime.Now,
                    DataAtualizacao = DateTime.Now,
                };
                await repository.Add(autor);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit([FromServices] AutorRepository repository, int? id)
        {
            var autorInfra = await repository.GetOne((int)id);
            if (id == null || autorInfra == null)
            {
                return NotFound();
            }
            else
            {
                var autor = new Autor()
                {
                    Id = autorInfra.Id,
                    Nome = autorInfra.Nome,
                    Sobrenome = autorInfra.Sobrenome,
                    DataRegistro = autorInfra.DataRegistro,
                    DataAtualizacao = autorInfra.DataAtualizacao
                };

                return View(autor);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit([FromServices] AutorRepository repository, Autor model)
        {
            if (ModelState.IsValid)
            {
                var autorInfra = new Infraestrutura.Infraestrutura.Models.Cadastro.Autor() {
                    Id = model.Id,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    DataAtualizacao = DateTime.Now,
                };
                await repository.Update(autorInfra);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete([FromServices] AutorRepository repository, int? id)
        {
            var autorInfra = await repository.GetOne((int)id);
            if(id==null || autorInfra == null)
            {
                return NotFound();
            }
            else
            {
                var autorModel = new Autor()
                {
                    Id = autorInfra.Id,
                    Nome = autorInfra.Nome,
                    Sobrenome = autorInfra.Sobrenome,
                    DataAtualizacao = autorInfra.DataAtualizacao,
                    DataRegistro = autorInfra.DataRegistro,
                };
                return View(autorModel);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete([FromServices] AutorRepository repository, int id)
        {
            try
            {
                await repository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details([FromServices] AutorRepository repository, int id)
        {
            if(await repository.GetOne((int)id) == null)
            {
                return NotFound();
            }
            else
            {
                var autorInfra = await repository.GetOne((int)id);
                var autorModel = new Autor()
                {
                    Id = autorInfra.Id,
                    Nome = autorInfra.Nome,
                    Sobrenome = autorInfra.Sobrenome,
                    DataRegistro = autorInfra.DataRegistro,
                    DataAtualizacao = autorInfra.DataAtualizacao,
                };
                return View(autorModel);
            }
        }
    }
}
