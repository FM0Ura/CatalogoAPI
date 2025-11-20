using AutoMapper;
using CatalogoAPI.DTOs.CategoriaDTO;
using CatalogoAPI.Models;
using CatalogoAPI.Pagination.Categorias;
using CatalogoAPI.Repositories.Unity_of_Work;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _unityOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CategoriasController> _logger;

    public CategoriasController(ILogger<CategoriasController> logger, IUnitOfWork unityOfWork, IMapper mapper)
    {
        _logger = logger;
        _unityOfWork = unityOfWork;
        _mapper = mapper;
    }

    [HttpGet(Name = "GetCategorias")]
    public async Task<ActionResult<IEnumerable<CategoriaDTOResponse>>> GetCategoriasAsync()
    {
        _logger.LogInformation("Consultando todas as categorias...");
        try
        {
            var categorias = await _unityOfWork.Categorias.GetAllAsync();
            if (categorias is null)
            {
                _logger.LogWarning("Nenhuma categoria encontrada.");
                return NotFound("Nenhuma categoria encontrada.");
            }

            return Ok(_mapper.Map<IEnumerable<CategoriaDTOResponse>>(categorias));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter as categorias");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet("filter/nome/pagination")]
    public async Task<ActionResult<IEnumerable<CategoriaDTOResponse>>> GetCategoriasFiltroNomeAsync([FromQuery] CategoriasFiltroNome categoriasParams)
    {
        _logger.LogInformation("Consultando todas as categorias...");
        try
        {
            var categorias = await _unityOfWork.Categorias.GetCategoriasFiltroNomeAsync(categoriasParams);
            if (categorias is null)
            {
                _logger.LogWarning("Nenhuma categoria encontrada.");
                return NotFound("Nenhuma categoria encontrada.");
            }

            return Ok(_mapper.Map<IEnumerable<CategoriaDTOResponse>>(categorias));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter as categorias");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet("{id:int}", Name = "GetCategoria")]
    public async Task<ActionResult<CategoriaDTOResponse>> GetCategoriaPorIdAsync(int id)
    {
        _logger.LogInformation("Consultando categoria com id={id}", id);
        try
        {
            var categoria = await _unityOfWork.Categorias.GetOneAsync(c => c.CategoriaId == id);

            if (categoria is null)
            {
                _logger.LogWarning("Categoria com id={id} não encontrada.", id);
                return NotFound("Categoria não encontrada.");
            }

            return Ok(_mapper.Map<CategoriaDTOResponse>(categoria));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter categoria com id={id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaDTOResponse>> CriarCategoriaAsync(CategoriaDTORequest categoriaDTO)
    {
        if (categoriaDTO is null)
        {
            _logger.LogWarning("Tentativa de criar uma categoria com dados nulos.");
            return BadRequest("Os dados da categoria não podem ser nulos.");
        }

        _logger.LogInformation("Criando nova categoria...");
        try
        {
            var categoria = _mapper.Map<Categoria>(categoriaDTO);
            var categoriaCriada = _unityOfWork.Categorias.Add(categoria);
            await _unityOfWork.CommitAsync();
            var categoriaCriadaDTO = _mapper.Map<CategoriaDTOResponse>(categoriaCriada);
            return CreatedAtAction(nameof(GetCategoriaPorIdAsync), new { id = categoriaCriadaDTO.CategoriaId }, categoriaCriadaDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar a nova categoria.");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoriaDTOResponse>> ModificarCategoriaAsync(int id, CategoriaDTORequest categoriaDTO)
    {
        if (categoriaDTO is null || id != categoriaDTO.Id)
        {
            _logger.LogWarning("Tentativa de modificar uma categoria com dados inválidos.");
            return BadRequest("Dados inválidos.");
        }

        _logger.LogInformation("Modificando categoria com id={id}", id);
        try
        {
            var categoria = _mapper.Map<Categoria>(categoriaDTO);
            var categoriaAtualizada = _unityOfWork.Categorias.Update(categoria);
            await _unityOfWork.CommitAsync();

            if (categoriaAtualizada is null)
            {
                _logger.LogWarning("Categoria com id={id} não encontrada para modificação.", id);
                return NotFound("Categoria não encontrada.");
            }

            return Ok(_mapper.Map<CategoriaDTOResponse>(categoriaAtualizada));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao modificar a categoria com id={id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoriaDTOResponse>> DeletarCategoria(int id)
    {
        var categoria = await _unityOfWork.Categorias.GetOneAsync(c => c.CategoriaId == id);
        if (categoria is null)
        {
            _logger.LogWarning("Tentativa de deletar uma categoria inexistente com id={id}.", id);
            return NotFound("Categoria não encontrada.");
        }

        _logger.LogInformation("Deletando categoria com id={id}", categoria.CategoriaId);
        try
        {
            var categoriaDeletada = _unityOfWork.Categorias.Delete(categoria);
            await _unityOfWork.CommitAsync();
            if (categoriaDeletada is null)
            {
                _logger.LogWarning("Categoria com id={id} não encontrada para deleção.", id);
                return NotFound("Categoria não encontrada.");
            }

            return Ok(_mapper.Map<CategoriaDTOResponse>(categoriaDeletada));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar a categoria com id={id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
}