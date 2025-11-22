using CatalogoAPI.Context;
using CatalogoAPI.Models;
using CatalogoAPI.Pagination;
using CatalogoAPI.Pagination.Categorias;
using CatalogoAPI.Repositories.Generic;

namespace CatalogoAPI.Repositories.Categorias;
public class CategoriasRepository : RepositoryGeneric<Categoria>, ICategoriasRepository
{
    public CategoriasRepository(CatalogoAPIContext context) : base(context)
    {
    }

    public async Task<PagedList<Categoria>> GetCategoriasAsync(QueryStringParameters categoriasParams)
    {
        var categorias = await GetAllAsync();

        var categoriasOrdenadas = categorias.OrderBy(c => c.CategoriaId).AsQueryable();

        var categoriasPaginadas = PagedList<Categoria>.ToPagedList(categoriasOrdenadas, categoriasParams.PageNumber, categoriasParams.PageSize);

        return categoriasPaginadas;
    }

    public async Task<PagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriasFiltroNome categoriasParams)
    {
        var categorias = await GetAllAsync();

        if (!string.IsNullOrEmpty(categoriasParams.Nome) && !string.IsNullOrEmpty(categoriasParams.CriterioBusca))
        { // "inicia", "contém", "termina", "igual", "não contém"
            if (categoriasParams.CriterioBusca.Equals("inicia" ,StringComparison.OrdinalIgnoreCase))
            {
                categorias = categorias.Where(c => c.Nome.StartsWith(categoriasParams.Nome));
            }
            if (categoriasParams.CriterioBusca.Equals("termina" ,StringComparison.OrdinalIgnoreCase))
            {
                categorias = categorias.Where(c => c.Nome.EndsWith(categoriasParams.Nome));
            }
            if (categoriasParams.CriterioBusca.Equals("contem" ,StringComparison.OrdinalIgnoreCase))
            {
                categorias = categorias.Where(c => c.Nome.Contains(categoriasParams.Nome));
            }
            if (categoriasParams.CriterioBusca.Equals("igual" ,StringComparison.OrdinalIgnoreCase))
            {
                categorias = categorias.Where(c => c.Nome.Equals(categoriasParams.Nome));
            }
            if (categoriasParams.CriterioBusca.Equals("nao contem" ,StringComparison.OrdinalIgnoreCase))
            {
                categorias = categorias.Where(c => c.Nome!.Contains(categoriasParams.Nome));
            }
        }

        var categoriasPaginadas = PagedList<Categoria>.ToPagedList(categorias.OrderBy(c => c.CategoriaId).AsQueryable(), categoriasParams.PageNumber, categoriasParams.PageSize);

        return categoriasPaginadas;
    }
}
