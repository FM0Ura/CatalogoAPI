using CatalogoAPI.Models;
using CatalogoAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriasRepository _repository;
    private readonly ILogger<CategoriasController> _logger;

    public CategoriasController(ICategoriasRepository repository, ILogger<CategoriasController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet(Name = "GetCategorias")]
    public ActionResult<IEnumerable<CategoriaModel>> GetCategorias()
    {
        _logger.LogInformation("Consultando todas as categorias...");
        try
        {
            var categorias = _repository.GetCategorias();
            if (categorias is null)
            {
                _logger.LogWarning("Nenhuma categoria encontrada.");
                return NotFound("Nenhuma categoria encontrada.");
            }

            return Ok(categorias);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter as categorias");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet("{id:int}", Name = "GetCategoria")]
    public ActionResult<CategoriaModel> GetCategoriaPorId(int id)
    {
        _logger.LogInformation("Consultando categoria com id={id}", id);
        try
        {
            var categoria = _repository.GetCategoria(id);

            if (categoria is null)
            {
                _logger.LogWarning("Categoria com id={id} não encontrada.", id);
                return NotFound("Categoria não encontrada.");
            }

            return Ok(categoria);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter categoria com id={id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<CategoriaModel>> GetCategoriasEProdutos()
    {
        _logger.LogInformation("Consultando categorias e seus produtos...");
        try
        {
            var categoriasEProdutos = _repository.GetCategoriasEProdutos();

            if (categoriasEProdutos is null)
            {
                _logger.LogWarning("Nenhuma categoria com produtos foi encontrada.");
                return NotFound("Nenhuma categoria com produtos foi encontrada.");
            }

            return Ok(categoriasEProdutos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter as categorias com produtos");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPost]
    public ActionResult CriarCategoria(CategoriaModel categoria)
    {
        if (categoria is null)
        {
            _logger.LogWarning("Tentativa de criar uma categoria com dados nulos.");
            return BadRequest("Os dados da categoria não podem ser nulos.");
        }

        _logger.LogInformation("Criando nova categoria...");
        try
        {
            var categoriaCriada = _repository.Create(categoria);
            return CreatedAtAction(nameof(GetCategoriaPorId), new { id = categoriaCriada.CategoriaId }, categoriaCriada);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar a nova categoria.");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPut("{id:int}")]
    public ActionResult ModificarCategoria(int id, CategoriaModel categoria)
    {
        if (categoria is null || id != categoria.CategoriaId)
        {
            _logger.LogWarning("Tentativa de modificar uma categoria com dados inválidos.");
            return BadRequest("Dados inválidos.");
        }

        _logger.LogInformation("Modificando categoria com id={id}", id);
        try
        {
            var categoriaAtualizada = _repository.Update(categoria);

            if (categoriaAtualizada is null)
            {
                _logger.LogWarning("Categoria com id={id} não encontrada para modificação.", id);
                return NotFound("Categoria não encontrada.");
            }

            return Ok(categoriaAtualizada);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao modificar a categoria com id={id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeletarCategoria(int id)
    {
        _logger.LogInformation("Deletando categoria com id={id}", id);
        try
        {
            var categoriaDeletada = _repository.Delete(id);

            if (categoriaDeletada is null)
            {
                _logger.LogWarning("Categoria com id={id} não encontrada para deleção.", id);
                return NotFound("Categoria não encontrada.");
            }

            return Ok(categoriaDeletada);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar a categoria com id={id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
}