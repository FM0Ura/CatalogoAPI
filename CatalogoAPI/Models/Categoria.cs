using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoAPI.Models;

[Table("Categorias")]
public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }

    [
        Required(ErrorMessage = "O nome da categoria é obrigatório."),
        StringLength(100, MinimumLength = 5,
        ErrorMessage = "O nome deve ter entre 5 e 100 caracteres.")
    ]
    public string Nome { get; set; }

    [
        Url(ErrorMessage = "A URL da imagem é inválida."),
        StringLength(300)
    ]
    public string? ImagemUrl { get; set; }
    public ICollection<Produto>? Produtos { get; set; }

    public Categoria(string nome)
    {
        Produtos = [];
        this.Nome = nome;
    }
}
