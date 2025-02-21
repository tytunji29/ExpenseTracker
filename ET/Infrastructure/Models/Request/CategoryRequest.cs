namespace Infrastructure.Models.Request;

public class CategoryRequest
{
    [Required]
    public string Name { get; set; }
}
