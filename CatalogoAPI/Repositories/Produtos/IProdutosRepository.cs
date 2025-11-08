using CatalogoAPI.Models;
using CatalogoAPI.Repositories.Generic;

namespace CatalogoAPI.Repositories.Produtos;
public interface IProdutosRepository : IRepositoryGeneric<ProdutoModel>
{
    IEnumerable<ProdutoModel> GetProdutosPorCategoria(int categoriaId);
}
