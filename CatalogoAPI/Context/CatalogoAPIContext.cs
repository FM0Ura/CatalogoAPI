using CatalogoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Context;

public class CatalogoAPIContext : DbContext
{
    public CatalogoAPIContext(DbContextOptions<CatalogoAPIContext> options ) : base(options)
    {
        
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
}
