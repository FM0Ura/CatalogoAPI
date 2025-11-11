namespace CatalogoAPI.DTOs.ProdutoDTO;

public class ProdutoDTOResponse
{
    public int ProdutoId { get; set; }
    public string? Nome { get; set; }
    public decimal Preco { get; set; }
    public int CategoriaId { get; set; }
}
