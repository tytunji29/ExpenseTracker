
namespace Infrastructure.Models.Dto;

public class ReturnObject
{
    public dynamic data { get; set; }
    public bool status { get; set; }
    public string message { get; set; }
}
public record MonthDto(
    int Id,
    string Name,
    int Year,
    List<IncomeDto> Incomes,
    List<ExpenseDto> Expenses
);

public record CatergoryDto(
    int Id,
    string Name
);
public record IncomeDto(
    int Id,
    string Source,
    decimal Amount,
    int monthid
);

public record ExpenseDto(
    int Id,
    string Name,
    decimal Amount,
    string CategoryName,
    string MonthName
);
