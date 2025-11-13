using CatalogoAPI.Context;
using CatalogoAPI.Models;
using CatalogoAPI.Pagination;
using CatalogoAPI.Repositories.Generic;

namespace CatalogoAPI.Repositories.Produtos;

public class ProdutosRepository : RepositoryGeneric<Produto>, IProdutosRepository
{
    public ProdutosRepository(CatalogoAPIContext context) : base(context)
    {
    }

    public PagedList<Produto> GetProdutos(ProdutosParameter produtosParameter)
    {
        var produtos = GetAll().OrderBy(c => c.ProdutoId).AsQueryable();
        var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos, produtosParameter.PageNumber, produtosParameter.PageSize);
        return produtosOrdenados;
    }

    public IEnumerable<Produto> GetProdutosPorCategoria(int categoriaId)
    {
        return GetAll().Where(c => c.CategoriaId == categoriaId);
    }
}
