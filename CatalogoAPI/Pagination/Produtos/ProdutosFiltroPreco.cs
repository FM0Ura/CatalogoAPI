namespace CatalogoAPI.Pagination.Produtos;

public class ProdutosFiltroPreco : ProdutosParameters
{
    public decimal? Preco { get; set; }
    public string? CriterioComparacao { get; set; }
}
