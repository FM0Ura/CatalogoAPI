using CatalogoAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CatalogoAPI.DTOs.ProdutoDTO;

public class ProdutoDTO
{
    [Required]
    public int ProdutoId { get; set; }

    [Required, StringLength(80)]
    public string? Nome { get; set; }

    [Required]
    public string? Descricao{ get; set; }

    [Required, Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }
    public int CategoriaId { get; set; }
}
