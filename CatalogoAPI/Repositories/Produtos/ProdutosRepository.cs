using CatalogoAPI.Context;
using CatalogoAPI.Models;
using CatalogoAPI.Pagination;
using CatalogoAPI.Pagination.Produtos;
using CatalogoAPI.Repositories.Generic;

namespace CatalogoAPI.Repositories.Produtos;

public class ProdutosRepository : RepositoryGeneric<Produto>, IProdutosRepository
{
    public ProdutosRepository(CatalogoAPIContext context) : base(context)
    {
    }

    public async Task<PagedList<Produto>> GetProdutosAsync(QueryStringParameters produtosParameter)
    {
        var produtos = await GetAllAsync();

        var produtosOrdenados = produtos.OrderBy(c => c.ProdutoId).AsQueryable();

        var produtosPaginados = PagedList<Produto>.ToPagedList(produtosOrdenados, produtosParameter.PageNumber, produtosParameter.PageSize);
        
        return produtosPaginados;
    }

    public async Task<PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosParameter)
    {
        var produtos = await GetAllAsync();

        var produtosOrdenados = produtos.OrderBy(c => c.ProdutoId).AsQueryable();

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

        var produtosPaginados = PagedList<Produto>.ToPagedList(produtosOrdenados, produtosParameter.PageNumber, produtosParameter.PageSize);
        
        return produtosPaginados;

    }

    public async Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int categoriaId)
    {
        var produtos = await GetAllAsync();

        var produtosECategorias = produtos.Where(c => c.CategoriaId == categoriaId).OrderBy(c => c.CategoriaId);

        return produtosECategorias;
    }
}
