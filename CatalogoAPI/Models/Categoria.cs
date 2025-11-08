using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoAPI.Models;

[Table("Categorias")]
public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }

    [Required, StringLength(80)]
    public string Nome { get; set; }

    [Required, StringLength(300)]
    public string? ImagemUrl { get; set; }
    public ICollection<Produto>? Produtos { get; set; }

    public Categoria(string nome)
    {
        Produtos = [];
        this.Nome = nome;
    }
}
