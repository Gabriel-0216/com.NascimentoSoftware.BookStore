using com.NascimentoSoftware.BookStore.API.Models;
using com.NascimentoSoftware.BookStore.Infraestrutura.Infraestrutura.Repositorios.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.NascimentoSoftware.BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<Autor>> Get([FromServices] AutorRepository autorRepository)
        {
            var listaAutores = new List<Autor>();
            var listaAutorInfra = await autorRepository.GetAll();
            foreach(var item in listaAutorInfra)
            {
                listaAutores.Add(new Autor()
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    DataAtualizacao = item.DataAtualizacao,
                    DataRegistro = item.DataRegistro,
                    Sobrenome = item.Sobrenome,
                });
            }
            return listaAutores;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromServices] AutorRepository autorRepo, int id)
        {
            var autor = await autorRepo.GetOne((int)id);
            if(autor == null)
            {
                return BadRequest();
            }
            var autorModel = new Autor()
            {
                Id = autor.Id,
                Nome = autor.Nome,
                DataAtualizacao = autor.DataAtualizacao,
                DataRegistro = autor.DataRegistro,
                Sobrenome = autor.Sobrenome,
            };
            return Ok(autorModel);
        }
    }
}
