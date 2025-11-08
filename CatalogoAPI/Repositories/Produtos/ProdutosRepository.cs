using CatalogoAPI.Context;
using CatalogoAPI.Models;
using CatalogoAPI.Repositories.Generic;

namespace CatalogoAPI.Repositories.Produtos;

public class ProdutosRepository : RepositoryGeneric<ProdutoModel>, IProdutosRepository
{
    public ProdutosRepository(CatalogoAPIContext context) : base(context)
    {
    }

    public IEnumerable<ProdutoModel> GetProdutosPorCategoria(int categoriaId)
    {
        return GetAll().Where(c => c.CategoriaId == categoriaId);
    }
}
