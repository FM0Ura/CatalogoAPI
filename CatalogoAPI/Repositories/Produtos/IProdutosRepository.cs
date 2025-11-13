using CatalogoAPI.Models;
using CatalogoAPI.Pagination;
using CatalogoAPI.Repositories.Generic;

namespace CatalogoAPI.Repositories.Produtos;
public interface IProdutosRepository : IRepositoryGeneric<Produto>
{
    PagedList<Produto> GetProdutos(ProdutosParameter produtosParameter);
    IEnumerable<Produto> GetProdutosPorCategoria(int categoriaId);
}
