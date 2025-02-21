
namespace Infrastructure.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    // Navigation property
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}

