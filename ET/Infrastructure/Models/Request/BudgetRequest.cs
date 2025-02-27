
namespace Infrastructure.Models.Request;

public class BudgetRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int Year { get; set; }
}
