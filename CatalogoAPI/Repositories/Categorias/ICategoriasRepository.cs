using CatalogoAPI.Models;
using CatalogoAPI.Pagination;
using CatalogoAPI.Repositories.Generic;

namespace CatalogoAPI.Repositories.Categorias;
public interface ICategoriasRepository : IRepositoryGeneric<Categoria>
{
    PagedList<Categoria> GetCategoriasFiltroNome(CategoriasFiltroNome categoriasParams);
}
