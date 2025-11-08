using CatalogoAPI.Models;
using CatalogoAPI.Repositories.Generic;

namespace CatalogoAPI.Repositories.Produtos;
public interface IProdutosRepository : IRepositoryGeneric<Produto>
{
    IEnumerable<Produto> GetProdutosPorCategoria(int categoriaId);
}
