using CatalogoAPI.Models;

namespace CatalogoAPI.Repositories;

public interface ICategoriasRepository
{
    IEnumerable<CategoriaModel> GetCategorias();
    CategoriaModel GetCategoria(int id);
    IEnumerable<CategoriaModel> GetCategoriasEProdutos();
    CategoriaModel Create(CategoriaModel categoria);
    CategoriaModel Update(CategoriaModel categoria);
    CategoriaModel Delete(int id);
}
