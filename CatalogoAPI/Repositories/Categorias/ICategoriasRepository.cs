using CatalogoAPI.Models;
using CatalogoAPI.Pagination;
using CatalogoAPI.Pagination.Categorias;
using CatalogoAPI.Repositories.Generic;

namespace CatalogoAPI.Repositories.Categorias;
public interface ICategoriasRepository : IRepositoryGeneric<Categoria>
{
    Task<PagedList<Categoria>> GetCategoriasAsync(QueryStringParameters categoriasParams);
    Task<PagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriasFiltroNome categoriasParams);
}
