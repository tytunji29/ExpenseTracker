
namespace Infrastructure.Models;

public class Income
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Source { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    // Foreign key
    public int MonthId { get; set; }

    // Navigation property
    public Month Month { get; set; } = null!;
}