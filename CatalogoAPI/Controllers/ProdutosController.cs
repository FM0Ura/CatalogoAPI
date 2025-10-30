using CatalogoAPI.Context;
using CatalogoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly CatalogoAPIContext _context;

    public ProdutosController(CatalogoAPIContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoModel>> GetProdutos()
    {
        var produtos = _context.Produtos.AsNoTracking().ToList();
        if (produtos is null)
        {
            return NotFound("Produtos não encontrados!");
        }
        return Ok(produtos);
    }


    [HttpGet("primeiro")]
    public ActionResult<ProdutoModel> GetPrimeiroProduto()
    {
        var produto = _context.Produtos.AsNoTracking().FirstOrDefault();
        if (produto is null)
        {
            return NotFound("Produtos não encontrados!");
        }
        return Ok(produto);
    }

    [HttpGet("{id:int:min(1)}", Name="ObterProduto")]
    public ActionResult<ProdutoModel> GetProduto(int id)
    {
        var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
        if (produto is null)
        {
            return NotFound($"Produto de ID {id} não encontrado!");
        }
        return Ok(produto);
    }

    [HttpPost]
    public ActionResult<ProdutoModel> PostProduto(ProdutoModel produto)
    {
        if (produto is null) return BadRequest("Produto vazio!");

        _context.Produtos.Add(produto);
        _context.SaveChanges();
            
        return new CreatedAtRouteResult("ObterProduto",
            new {id = produto.ProdutoId}, produto);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<ProdutoModel> AlterarProduto(int id, ProdutoModel produto)
    {
        if (id != produto.ProdutoId)
        {
            return BadRequest($"ID informado {id} diferente de ProdutoID!");   
        }

        _context.Produtos.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();
            
        return Ok(produto);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<ProdutoModel> DeletarProduto(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if (produto is null)
        {
            return NotFound("O ID informado não consta no Banco de Dados!");
        }

        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return Ok(produto);
    }
}
