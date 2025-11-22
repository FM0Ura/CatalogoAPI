using CatalogoAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Context;

public class CatalogoAPIContext : IdentityDbContext
{
    public CatalogoAPIContext(DbContextOptions<CatalogoAPIContext> options ) : base(options)
    {
        
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
}
