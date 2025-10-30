using CatalogoAPI.Context;
using CatalogoAPI.Filters;
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
    private readonly ILogger<CategoriasController> _logger;

    public CategoriasController(CatalogoAPIContext context, ILogger<CategoriasController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CategoriaModel>> GetCategorias()
    {
        _logger.LogInformation("Buscando todas as categorias");
        var categorias = _context.Categorias.AsNoTracking().ToList();
        if (categorias is null || !categorias.Any())
        {
            _logger.LogWarning("Nenhuma categoria encontrada");
            return NotFound("Categorias não encontradas!");
        }
        _logger.LogInformation("Retornando {Count} categorias", categorias.Count);
        return categorias;

    }

    [HttpGet("{id:int}")]
    public ActionResult GetCategoriaPorId(int id)
    {

        _logger.LogInformation("Buscando categoria pelo ID: {Id}", id);
        var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(p => p.CategoriaId == id);
        if (categoria is null)
        {
            _logger.LogWarning("Categoria com ID {Id} não encontrada", id);
            return NotFound($"Categoria de ID {id} não encontrada!");
        }
        _logger.LogInformation("Retornando categoria com ID {Id}", id);
        return Ok(categoria);

    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<CategoriaModel>> GetCategoriasEProdutos()
    {

        _logger.LogInformation("Buscando categorias e seus produtos");
        var categoriasComProdutos = _context.Categorias.AsNoTracking().Include(p => p.Produtos).ToList();
        _logger.LogInformation("Retornando {Count} categorias com produtos", categoriasComProdutos.Count);
        return categoriasComProdutos;
    }

    [HttpPost]
    public ActionResult CriarCategoria(CategoriaModel categoria)
    {

            if (categoria is null)
            {
                _logger.LogWarning("Tentativa de criar uma categoria nula");
                return BadRequest("Categoria vazia!");
            }
            _logger.LogInformation("Criando nova categoria");
            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            _logger.LogInformation("Categoria com ID {Id} criada com sucesso", categoria.CategoriaId);
            return CreatedAtAction(nameof(GetCategoriaPorId),
                new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut("{id:int}")]
    public ActionResult ModificarCategoria(int id, CategoriaModel categoria)
    {
        try
        {
            if (id != categoria.CategoriaId)
            {
                _logger.LogWarning("ID do corpo {CategoriaId} não corresponde ao ID da rota {Id}", categoria.CategoriaId, id);
                return BadRequest($"ID informado {id} diferente de CategoriaID!");
            }

            _logger.LogInformation("Modificando categoria com ID: {Id}", id);
            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            _logger.LogInformation("Categoria com ID {Id} modificada com sucesso", id);
            return Ok(categoria);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError(ex, "Erro de concorrência ao modificar a categoria com ID {Id}", id);
            return NotFound($"Categoria de ID {id} não encontrada!");
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeletarCategoria(int id)
    {

        _logger.LogInformation("Deletando categoria com ID: {Id}", id);
        var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

        if (categoria is null)
        {
            _logger.LogWarning("Categoria com ID {Id} não encontrada para exclusão", id);
            return NotFound($"Categoria de ID {id} não encontrada!");
        }

        _context.Categorias.Remove(categoria);
        _context.SaveChanges();

        _logger.LogInformation("Categoria com ID {Id} deletada com sucesso", id);
        return Ok(categoria);

    }

}