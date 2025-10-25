using CatalogoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Context;

public class CatalogoAPIContext : DbContext
{
    public CatalogoAPIContext(DbContextOptions<CatalogoAPIContext> options ) : base(options)
    {
        
    }

    public DbSet<ProdutoModel> Produtos { get; set; }
    public DbSet<CategoriaModel> Categorias { get; set; }
}
