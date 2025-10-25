namespace CatalogoAPI.Models;

public class CategoriaModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string? ImagemUrl { get; set; }
    public ICollection<ProdutoModel>? Produtos { get; set; }

    public CategoriaModel(string nome)
    {
        Produtos = [];
        this.Nome = nome;
    }
}
