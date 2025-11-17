namespace CatalogoAPI.Pagination;

public class CategoriasFiltroNome : CategoriasParameters
{
    public string? Nome { get; set; }
    public string? CriterioBusca { get; set; } // "inicia", "contém", "termina", "igual", "não contém"
}
