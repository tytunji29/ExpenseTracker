using Infrastructure.Data;
using Infrastructure.Models.Request;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
string? conn = builder.Configuration.GetConnectionString("NpocoConn");
builder.Services.Configure<DataAccess>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NpocoConn")));

builder.Services.AddScoped<IMonthRepository, MonthRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
builder.Services.AddOpenApi();
var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference(x =>
{
    x.WithTitle("Expenses Tracker API");
    x.WithTheme(ScalarTheme.DeepSpace);
    x.WithSidebar(true);
});
app.UseHttpsRedirection();

var categoryApi = app.MapGroup("/api/catergories");

categoryApi.MapGet("/", async (ICategoryRepository _repo) =>
{
    var months = await _repo.GetAllCategory();
    return Results.Ok(months);
});
categoryApi.MapPost("/", async (ICategoryRepository _repo, CategoryRequest monthDto) =>
{
    var newMonth = await _repo.AddCategory(monthDto);
    return Results.Ok(newMonth);
});

var IncomeApi = app.MapGroup("/api/incomes");

IncomeApi.MapGet("/", async (IIncomeRepository _repo) =>
{
    var months = await _repo.GetAllIncome();
    return Results.Ok(months);
});
IncomeApi.MapPost("/", async (IIncomeRepository _repo, IncomeRequest monthDto) =>
{
    var newMonth = await _repo.AddIncome(monthDto);
    return Results.Ok(newMonth);
});

var ExpensesApi = app.MapGroup("/api/Expenses");
ExpensesApi.MapGet("/", async (IExpenseRepository _repo) =>
{
    var Expenses = await _repo.GetAllExpense();
    return Results.Ok(Expenses);
});
ExpensesApi.MapPost("/", async (IExpenseRepository _repo, ExpenseRequest ExpenseDto) =>
{
    var newExpense = await _repo.AddExpense(ExpenseDto);
    return Results.Ok(newExpense);
});
var monthApi = app.MapGroup("/api/months");
monthApi.MapGet("/", async (IMonthRepository _repo) =>
{
    var month = await _repo.GetAllMonth();
    return Results.Ok(month);
});
monthApi.MapPost("/", async (IMonthRepository _repo, MonthRequest ExpenseDto) =>
{
    var newExpense = await _repo.AddMonth(ExpenseDto);
    return Results.Ok(newExpense);
});

app.Run();

