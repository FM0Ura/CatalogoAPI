using AutoMapper;
using CatalogoAPI.DTOs.ProdutoDTO;
using CatalogoAPI.Models;
using CatalogoAPI.Pagination;
using CatalogoAPI.Repositories.Unity_of_Work;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _unityOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ProdutosController> _logger;

    public ProdutosController(ILogger<ProdutosController> logger, IUnitOfWork unityOfWork, IMapper mapper)
    {
        _logger = logger;
        _unityOfWork = unityOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTOResponse>> GetProdutos()
    {
        _logger.LogInformation("Consultando todos os produtos...");
        try
        {
            var produtos = _unityOfWork.Produtos.GetAll();
            if (produtos is null || !produtos.Any())
            {
                _logger.LogWarning("Nenhum produto encontrado.");
                return NotFound("Nenhum produto encontrado.");
            }

            var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTOResponse>>(produtos);

            return Ok(produtosDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter os produtos.");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet("pagination")]
    public ActionResult<IEnumerable<ProdutoDTOResponse>> GetProdutosPaginados([FromQuery] Pagination.QueryStringParameters produtosParams)
    {
        _logger.LogInformation("Consultando produtos paginados...");
        try
        {
            var produtosPaginados = _unityOfWork.Produtos.GetProdutos(produtosParams);

            var metadata = new
            {
                produtosPaginados.TotalCount,
                produtosPaginados.PageSize,
                produtosPaginados.CurrentPage,
                produtosPaginados.TotalPages,
                produtosPaginados.HasNext,
                produtosPaginados.HasPrevious
            };

            Response.Headers.Append("X-Pagination",
                System.Text.Json.JsonSerializer.Serialize(metadata));
            var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTOResponse>>(produtosPaginados);
            return Ok(produtosDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter os produtos paginados.");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
    public ActionResult<ProdutoDTOResponse> GetProduto(int id)
    {
        _logger.LogInformation("Consultando produto com id={id}", id);
        try
        {
            var produto = _unityOfWork.Produtos.GetOne(c=> c.ProdutoId == id);
            if (produto is null)
            {
                _logger.LogWarning("Produto com id={id} não encontrado.", id);
                return NotFound("Produto não encontrado.");
            }

            var produtoDTO = _mapper.Map<ProdutoDTOResponse>(produto);

            return Ok(produtoDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter o produto com id={id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet("categoria/{categoriaId:int}")]
    public ActionResult<IEnumerable<ProdutoDTOResponse>> GetProdutosporCategoria(int categoriaId)
    {
        _logger.LogInformation("Consultando categorias e seus produtos...");
        try
        {
            var categoriasEProdutos = _unityOfWork.Produtos.GetProdutosPorCategoria(categoriaId);
            Console.WriteLine(categoriasEProdutos);
            if (categoriasEProdutos is null || !categoriasEProdutos.Any())
            {
                _logger.LogWarning("Nenhuma categoria com produtos foi encontrada.");
                return NotFound("Nenhuma categoria com produtos foi encontrada.");
            }

            return Ok(_mapper.Map<IEnumerable<ProdutoDTOResponse>>(categoriasEProdutos));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter as categorias com produtos");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPost]
    public ActionResult<ProdutoDTOResponse> PostProduto(ProdutoDTORequest produtoDTO)
    {
        if (produtoDTO is null)
        {
            _logger.LogWarning("Tentativa de criar um produto com dados nulos.");
            return BadRequest("Os dados do produto não podem ser nulos.");
        }

        _logger.LogInformation("Criando novo produto...");
        try
        {
            var produto = _mapper.Map<Produto>(produtoDTO);

            var produtoCriado = _unityOfWork.Produtos.Add(produto);
            _unityOfWork.Commit();

            var novoProdutoDTO = _mapper.Map<ProdutoDTOResponse>(produtoCriado);

            return new CreatedAtRouteResult("ObterProduto",
                new { id = novoProdutoDTO.ProdutoId }, novoProdutoDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar o novo produto.");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<ProdutoDTOResponse> AlterarProduto(int id, ProdutoDTORequest produtoDTO)
    {
        if (produtoDTO is null || id != produtoDTO.ProdutoId)
        {
            _logger.LogWarning("Tentativa de modificar um produto com dados inválidos.");
            return BadRequest("Dados inválidos.");
        }

        _logger.LogInformation("Modificando produto com id={id}", id);
        try
        {
            var produto = _mapper.Map<Produto>(produtoDTO);
            var produtoAtualizado = _unityOfWork.Produtos.Update(produto);
            _unityOfWork.Commit();

            return Ok(_mapper.Map<ProdutoDTOResponse>(produtoAtualizado));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao modificar o produto com id={id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoDTOResponse> DeletarProduto(int id)
    {
        _logger.LogInformation("Deletando produto com id={id}", id);
        var produto = _unityOfWork.Produtos.GetOne(c => c.ProdutoId == id);

        if (produto is null)
        { 
            _logger.LogWarning("Produto com id={id} não encontrado para deleção.", id);
            return NotFound("Produto não encontrado.");
        }
        try
        {
            var produtoDeletado = _unityOfWork.Produtos.Delete(produto);
            _unityOfWork.Commit();
            if (produtoDeletado is null)
            {
                return NotFound("Produto não encontrado.");
            }

            return Ok(_mapper.Map<ProdutoDTOResponse>(produtoDeletado));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
}
