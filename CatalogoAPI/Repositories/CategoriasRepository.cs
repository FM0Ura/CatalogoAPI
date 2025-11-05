using CatalogoAPI.Context;
using CatalogoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CatalogoAPI.Repositories;

public class CategoriasRepository : ICategoriasRepository
{
    private readonly CatalogoAPIContext _context;

    public CategoriasRepository(CatalogoAPIContext context)
    {
        _context = context;
    }

    public CategoriaModel Create(CategoriaModel categoria)
    {
        _context.Categorias.Add(categoria);
        _context.SaveChanges();
        return categoria;
    }

    public CategoriaModel Delete(int id)
    {
        var remover = this.GetCategoria(id);
        _context.Categorias.Remove(remover);
        _context.SaveChanges();
        return remover;
    }

    public CategoriaModel GetCategoria(int id)
    {
        return _context.Categorias.AsNoTracking().FirstOrDefault(predicate: c => c.CategoriaId == id);
    }

    public IEnumerable<CategoriaModel> GetCategorias()
    {
        return _context.Categorias.AsNoTracking().ToList();
    }

    public IEnumerable<CategoriaModel> GetCategoriasEProdutos()
    {
        return _context.Categorias
            .AsNoTracking()
            .Include(c => c.Produtos)
            .ToList();
    }

    public CategoriaModel Update(CategoriaModel categoria)
    {
        _context.Categorias.Entry(Update(categoria)).State = EntityState.Modified;
        _context.SaveChanges();
        return categoria;
    }
}
