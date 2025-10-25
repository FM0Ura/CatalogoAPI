using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoAPI.Models;

[Table("Categorias")]
public class CategoriaModel
{
    [Key]
    public int CategoriaId { get; set; }

    [Required, StringLength(80)]
    public string Nome { get; set; }

    [Required, StringLength(300)]
    public string? ImagemUrl { get; set; }
    public ICollection<ProdutoModel>? Produtos { get; set; }

    public CategoriaModel(string nome)
    {
        Produtos = [];
        this.Nome = nome;
    }
}
