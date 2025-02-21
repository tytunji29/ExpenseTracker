namespace Infrastructure.Models.Request;

public class IncomeRequest
{
    public string Source { get; set; }
    public decimal Amount { get; set; }
    public int MonthId { get; set; }
}
