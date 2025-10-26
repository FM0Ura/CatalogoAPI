using CatalogoAPI.Context;
using CatalogoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly CatalogoAPIContext _context;

    public CategoriasController(CatalogoAPIContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CategoriaModel>> GetCategorias()
    {
        try
        {
            var categorias = _context.Categorias.AsNoTracking().ToList();
            if (categorias is null)
            {
                return NotFound("Categorias não encontradas!");
            }
            return categorias;
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro no servidor, por favor entre em contato com o Dev responsável FM0Ura.");
        }
    }

    [HttpGet("{id:int}")]
    public ActionResult GetCategoriaPorId(int id)
    {
        try
        {
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(p => p.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound($"Categoria de ID {id} não encontrada!");
            }
            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro no servidor, por favor entre em contato com o Dev responsável FM0Ura.");
        }
    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<CategoriaModel>> GetCategoriasEProdutos()
    {
        try
        {
            return _context.Categorias.AsNoTracking().Include(p => p.Produtos).ToList();
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro no servidor, por favor entre em contato com o Dev responsável FM0Ura.");
        }
    }

    [HttpPost]
    public ActionResult CriarCategoria(CategoriaModel categoria)
    {
        try
        {
            if (categoria is null) return BadRequest("Categoria vazia!");
            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCategoriaPorId),
                new { id = categoria.CategoriaId }, categoria);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro no servidor, por favor entre em contato com o Dev responsável FM0Ura.");
        }
    }

    [HttpPut("{id:int}")]
    public ActionResult ModificarCategoria(int id, CategoriaModel categoria)
    {
        try
        {
            if (_context.Categorias.FirstOrDefault(p => p.CategoriaId == id) is null) return NotFound($"Categoria de ID {id} não encontrada!");

            if (id != categoria.CategoriaId)
            {
                return BadRequest($"ID informado {id} diferente de CategoriaID!");
            }
            _context.Categorias.Update(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro no servidor, por favor entre em contato com o Dev responsável FM0Ura.");
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeletarCategoria(int id)
    {
        try
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            if (categoria is null) return NotFound($"Categoria de ID {id} não encontrada!");

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocorreu um erro no servidor, por favor entre em contato com o Dev responsável FM0Ura.");
        }
    }

}
