using Infrastructure.Data;
using Infrastructure.Models.Request;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
string? conn = builder.Configuration.GetConnectionString("NpocoConn");
builder.Services.Configure<DataAccess>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NpocoConn")));

builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
builder.Services.AddOpenApi();
//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(7229); // Ensure it listens on all IPs
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference(x =>
{
    x.WithTitle("Expenses Tracker API");
    x.WithTheme(ScalarTheme.DeepSpace);
    x.WithSidebar(true);
});

app.UseCors("AllowAll");
app.UseHttpsRedirection();

var categoryApi = app.MapGroup("/api/categories");

categoryApi.MapGet("/", async (ICategoryRepository _repo) =>
{
    var Budgets = await _repo.GetAllCategory();
    return Results.Ok(Budgets);
});
categoryApi.MapPost("/", async (ICategoryRepository _repo, CategoryRequest BudgetDto) =>
{
    var newBudget = await _repo.AddCategory(BudgetDto);
    return Results.Ok(newBudget);
});

var IncomeApi = app.MapGroup("/api/incomes");

IncomeApi.MapGet("/", async (IIncomeRepository _repo) =>
{
    var Budgets = await _repo.GetAllIncome();
    return Results.Ok(Budgets);
});
IncomeApi.MapPost("/", async (IIncomeRepository _repo, IncomeRequest BudgetDto) =>
{
    var newBudget = await _repo.AddIncome(BudgetDto);
    return Results.Ok(newBudget);
});

var ExpensesApi = app.MapGroup("/api/Expenses");
ExpensesApi.MapGet("/{pageSize}/{pageNumber}", async ([FromRoute] int pageSize, [FromRoute] int pageNumber, IExpenseRepository _repo) =>
{
    var Expenses = await _repo.GetAllExpense(pageSize, pageNumber);
    return Results.Ok(Expenses);
});
ExpensesApi.MapPost("/", async (IExpenseRepository _repo, ExpenseRequest ExpenseDto) =>
{
    var newExpense = await _repo.AddExpense(ExpenseDto);
    return Results.Ok(newExpense);
});
ExpensesApi.MapPut("/UpdateStatus/{id}", async ([FromRoute] int id, IExpenseRepository _repo, ExpenseRequestStatus ExpenseDto) =>
{
    var newExpense = await _repo.UpdateExpenseStatus(id, ExpenseDto);
    return Results.Ok(newExpense);
});
var monthApi = app.MapGroup("/api/Months");
monthApi.MapGet("/", async (IBudgetRepository _repo) =>
{
    var Budget = await _repo.GetAllMonth();
    return Results.Ok(Budget);
});
monthApi.MapPost("/", async (IBudgetRepository _repo, BudgetRequest ExpenseDto) =>
{
    var newExpense = await _repo.AddBudget(ExpenseDto);
    return Results.Ok(newExpense);
});
monthApi.MapPut("/{id}", async ([FromRoute] int id, IBudgetRepository _repo, BudgetRequest ExpenseDto) =>
{
    var newExpense = await _repo.UpdateMonth(id, ExpenseDto);
    return Results.Ok(newExpense);
});
var BudgetApi = app.MapGroup("/api/Budgets");
BudgetApi.MapGet("/", async (IBudgetRepository _repo) =>
{
    var Budget = await _repo.GetAllBudget();
    return Results.Ok(Budget);
});
BudgetApi.MapPost("/", async (IBudgetRepository _repo, BudgetRequest ExpenseDto) =>
{
    var newExpense = await _repo.AddBudget(ExpenseDto);
    return Results.Ok(newExpense);
});

app.Run();

