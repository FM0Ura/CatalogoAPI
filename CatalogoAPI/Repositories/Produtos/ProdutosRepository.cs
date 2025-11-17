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

    public PagedList<Produto> GetProdutos(QueryStringParameters produtosParameter)
    {
        var produtos = GetAll().OrderBy(c => c.ProdutoId).AsQueryable();
        var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos, produtosParameter.PageNumber, produtosParameter.PageSize);
        return produtosOrdenados;
    }

    public PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosParameter)
    {
        var produtos = GetAll().OrderBy(c => c.ProdutoId).AsQueryable();

        if (produtosParameter.Preco.HasValue && !string.IsNullOrEmpty(produtosParameter.CriterioComparacao))
        {
            if (produtosParameter.CriterioComparacao.Equals("maior", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco > produtosParameter.Preco.Value).OrderBy(p => p.ProdutoId);
            }
            if (produtosParameter.CriterioComparacao.Equals("menor", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco < produtosParameter.Preco.Value).OrderBy(p => p.ProdutoId);
            }
            if (produtosParameter.CriterioComparacao.Equals("igual", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco == produtosParameter.Preco.Value).OrderBy(p => p.ProdutoId);
            }
        }

        var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos, produtosParameter.PageNumber, produtosParameter.PageSize);
        
        return produtosOrdenados;

    }

    public IEnumerable<Produto> GetProdutosPorCategoria(int categoriaId)
    {
        return GetAll().Where(c => c.CategoriaId == categoriaId);
    }
}
