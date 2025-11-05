using CatalogoAPI.Models;
using CatalogoAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CatalogoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutosRepository _repository;
    private readonly ILogger<ProdutosController> _logger;

    public ProdutosController(IProdutosRepository repository, ILogger<ProdutosController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoModel>> GetProdutos()
    {
        _logger.LogInformation("Consultando todos os produtos...");
        try
        {
            var produtos = _repository.GetProdutos();
            if (produtos is null || !produtos.Any())
            {
                _logger.LogWarning("Nenhum produto encontrado.");
                return NotFound("Nenhum produto encontrado.");
            }
            return Ok(produtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter os produtos.");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet("{id:int:min(1)}")]
    public ActionResult<ProdutoModel> GetProduto(int id)
    {
        _logger.LogInformation("Consultando produto com id={id}", id);
        try
        {
            var produto = _repository.GetProduto(id);
            if (produto is null)
            {
                _logger.LogWarning("Produto com id={id} não encontrado.", id);
                return NotFound("Produto não encontrado.");
            }
            return Ok(produto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter o produto com id={id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPost]
    public ActionResult<ProdutoModel> PostProduto(ProdutoModel produto)
    {
        if (produto is null)
        {
            _logger.LogWarning("Tentativa de criar um produto com dados nulos.");
            return BadRequest("Os dados do produto não podem ser nulos.");
        }

        _logger.LogInformation("Criando novo produto...");
        try
        {
            var produtoCriado = _repository.PostProduto(produto);
            return new CreatedAtRouteResult("ObterProduto",
                new { id = produtoCriado.ProdutoId }, produtoCriado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar o novo produto.");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<ProdutoModel> AlterarProduto(int id, ProdutoModel produto)
    {
        if (produto is null || id != produto.ProdutoId)
        {
            _logger.LogWarning("Tentativa de modificar um produto com dados inválidos.");
            return BadRequest("Dados inválidos.");
        }

        _logger.LogInformation("Modificando produto com id={id}", id);
        try
        {
            var produtoAtualizado = _repository.Modify(produto);
            return Ok(produtoAtualizado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao modificar o produto com id={id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeletarProduto(int id)
    {
        try
        {
            var produtoDeletado = _repository.Delete(id);

            if (produtoDeletado is null)
            {
                return NotFound("Produto não encontrado.");
            }

            return Ok(produtoDeletado);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
}
