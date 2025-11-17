using CatalogoAPI.Context;
using CatalogoAPI.Models;
using CatalogoAPI.Pagination;
using CatalogoAPI.Repositories.Generic;

namespace CatalogoAPI.Repositories.Categorias;
public class CategoriasRepository : RepositoryGeneric<Categoria>, ICategoriasRepository
{
    public CategoriasRepository(CatalogoAPIContext context) : base(context)
    {
    }

    public PagedList<Categoria> GetCategoriasFiltroNome(CategoriasFiltroNome categoriasParams)
    {
        var categorias = _context.Categorias.AsQueryable();

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
        return PagedList<Categoria>.ToPagedList(categorias, categoriasParams.PageNumber, categoriasParams.PageSize);
    }
}
