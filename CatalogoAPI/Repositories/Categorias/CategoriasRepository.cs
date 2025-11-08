using CatalogoAPI.Context;
using CatalogoAPI.Models;
using CatalogoAPI.Repositories.Generic;

namespace CatalogoAPI.Repositories.Categorias;
public class CategoriasRepository : RepositoryGeneric<CategoriaModel>, ICategoriasRepository
{
    public CategoriasRepository(CatalogoAPIContext context) : base(context)
    {
    }
}
