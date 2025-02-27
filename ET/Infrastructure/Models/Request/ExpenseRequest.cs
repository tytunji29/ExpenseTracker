
namespace Infrastructure.Models.Request;

public class ExpenseRequestStatus
{
    public string Status { get; set; }
}
public class ExpenseRequest
{
    public string Name { get; set; }

    public decimal Amount { get; set; }
    public int CategoryId { get; set; }
    public int MonthId { get; set; }
}
