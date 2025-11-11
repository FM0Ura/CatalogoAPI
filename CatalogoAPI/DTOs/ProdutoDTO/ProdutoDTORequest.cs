namespace CatalogoAPI.DTOs.ProdutoDTO;

public class ProdutoDTORequest
{
    public int ProdutoId { get; set; }
    public string? Nome { get; set; }
    public decimal Preco { get; set; }
    public float Estoque { get; set; }
    public int CategoriaId { get; set; }
}
