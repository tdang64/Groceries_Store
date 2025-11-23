using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Products")]
public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id  { get; set; }

    public int? CategoryId { get; set; }

    [Required]
    [MaxLength(255)]
    public string? Name { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Price { get; set; }

    public string? Description { get; set; } = string.Empty;
}
