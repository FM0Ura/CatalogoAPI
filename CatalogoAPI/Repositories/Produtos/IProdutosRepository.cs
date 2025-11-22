using CatalogoAPI.Models;
using CatalogoAPI.Pagination;
using CatalogoAPI.Pagination.Produtos;
using CatalogoAPI.Repositories.Generic;

namespace CatalogoAPI.Repositories.Produtos;
public interface IProdutosRepository : IRepositoryGeneric<Produto>
{
    Task<PagedList<Produto>> GetProdutosAsync(QueryStringParameters produtosParameter);
    Task<PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosParameter);
    Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int categoriaId);
}
