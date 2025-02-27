
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
string FullMonth,
    int Year);
public record BudgetDto(
int Id,
string Name,
int Year,
List<IncomeDto> Incomes,
List<ExpenseDto> Expenses,
decimal TotalIncome,
decimal TotalExpense
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
    string Status,
    decimal Amount,
    string CategoryName,
    string MonthName,
    int year
);
