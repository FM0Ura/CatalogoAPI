using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CatalogoAPI.Models;

[Table("Produtos")]
public class Produto : IValidatableObject
{
    [Key]
    public int ProdutoId { get; set; }

    [
        Required(ErrorMessage = "O nome do produto é obrigatório."),
        StringLength(100, MinimumLength = 3,
        ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")
    ]
    public string? Nome { get; set; }

    [
        Required(ErrorMessage = "O nome do produto é obrigatório."), 
        StringLength(300, MinimumLength = 1,
        ErrorMessage = "A descrição deve ter entre 1 e 100 caracteres.")
    ]
    public string? Descricao { get; set; }

    [
        Required(ErrorMessage = "O preço do produto é obrigatório."),
        Column(TypeName ="decimal(10,2)")
    ]
    public decimal Preco { get; set; }

    [StringLength(300)]
    public string? ImagemUrl { get; set; }

    [Range(0, 2000000, ErrorMessage = "O estoque não pode ser negativo.")]
    public float Estoque { get; set; }

    [JsonIgnore]
    public DateTime DataCadastro { get; set; } = DateTime.Now;

    [ForeignKey("CategoriaId")]
    public int CategoriaId { get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(this.Nome))
        {
            var primeiraLetra = this.Nome[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return new ValidationResult("A primeira letra do produto deve ser maiúscula.",
                    new[]
                    { nameof(this.Nome) });
            }
        }

        if (this.Estoque < 0)
        {
            yield return new ValidationResult("Estoque não deve ser negativo", new[] { nameof(this.Estoque) });
        }
    }
}
