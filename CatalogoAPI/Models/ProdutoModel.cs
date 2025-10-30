﻿using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;

namespace CatalogoAPI.Models;

[Table("Produtos")]
public class ProdutoModel : IValidatableObject
{
    [Key]
    public int ProdutoId { get; set; }

    [Required, StringLength(80)]
    public string? Nome { get; set; }

    [Required, StringLength(300)]
    public string? Descricao { get; set; }

    [Required, Column(TypeName ="decimal(10,2)")]
    public decimal Preco { get; set; }

    [Required, StringLength(300)]
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }

    [JsonIgnore]
    public DateTime DataCadastro { get; set; } = DateTime.Now;

    [ForeignKey("CategoriaId")]
    public int CategoriaId { get; set; }

    [JsonIgnore]
    public CategoriaModel? Categoria { get; set; }

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
