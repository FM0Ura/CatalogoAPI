using CatalogoAPI.Models;

namespace CatalogoAPI.Repositories;

public interface IProdutosRepository
{
    ProdutoModel GetProduto(int id);
    IEnumerable<ProdutoModel> GetProdutos();
    ProdutoModel PostProduto(ProdutoModel produto);
    ProdutoModel Modify(ProdutoModel produto);
    ProdutoModel Delete(int id);
}
