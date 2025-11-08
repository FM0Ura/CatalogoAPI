using CatalogoAPI.Context;
using CatalogoAPI.Models;
using CatalogoAPI.Repositories.Generic;

namespace CatalogoAPI.Repositories.Produtos;

public class ProdutosRepository : RepositoryGeneric<Produto>, IProdutosRepository
{
    public ProdutosRepository(CatalogoAPIContext context) : base(context)
    {
    }

    public IEnumerable<Produto> GetProdutosPorCategoria(int categoriaId)
    {
        return GetAll().Where(c => c.CategoriaId == categoriaId);
    }
}
