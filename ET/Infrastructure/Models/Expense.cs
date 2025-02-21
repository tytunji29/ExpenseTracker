

namespace Infrastructure.Models;


public class Expense
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    // Foreign keys
    public int CategoryId { get; set; }
    public int MonthId { get; set; }

    // Navigation properties
    public Category Category { get; set; } = null!;
    public Month Month { get; set; } = null!;
}

