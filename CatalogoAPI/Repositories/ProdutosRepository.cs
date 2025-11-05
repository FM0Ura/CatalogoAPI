using CatalogoAPI.Context;
using CatalogoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Repositories;

public class ProdutosRepository : IProdutosRepository
{
    private readonly CatalogoAPIContext _context;

    public ProdutosRepository(CatalogoAPIContext context)
    {
        _context = context;
    }

    public ProdutoModel Modify(ProdutoModel produto)
    {
        _context.Produtos.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();
        return produto;
    }

    public ProdutoModel Delete(int id)
    {
        var remover = this.GetProduto(id);
        _context.Produtos.Remove(remover);
        _context.SaveChanges();
        return remover;
    }

    public ProdutoModel GetProduto(int id)
    {
        return _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
    }

    public IEnumerable<ProdutoModel> GetProdutos()
    {
        return _context.Produtos.AsNoTracking().ToList();
    }

    public ProdutoModel PostProduto(ProdutoModel produto)
    {
        _context.Produtos.Add(produto);
        _context.SaveChanges();
        return produto;
    }
}
