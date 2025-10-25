using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoAPI.Models;

[Table("Produtos")]
public class ProdutoModel
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(80)]
    public string? Nome { get; set; }

    [Required, StringLength(300)]
    public string? Descricao { get; set; }

    [Required, Column(TypeName ="decimal(10,2)")]
    public decimal Preco { get; set; }

    [Required, StringLength(300)]
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }

    [ForeignKey("CategoriaId")]
    public int CategoriaId { get; set; }
    public CategoriaModel? Categoria { get; set; }
}
