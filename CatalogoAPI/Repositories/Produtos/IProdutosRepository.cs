using CatalogoAPI.Models;
using CatalogoAPI.Pagination;
using CatalogoAPI.Repositories.Generic;

namespace CatalogoAPI.Repositories.Produtos;
public interface IProdutosRepository : IRepositoryGeneric<Produto>
{
    PagedList<Produto> GetProdutos(QueryStringParameters produtosParameter);
    PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosParameter);
    IEnumerable<Produto> GetProdutosPorCategoria(int categoriaId);
}
