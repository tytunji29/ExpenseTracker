namespace Infrastructure.Services;

public static class AllService
{
    public static async Task<List<ExpenseDto>> GetAllExpenseAsync(IDbContextFactory<AppDbContext> _contextFactory)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("MonthService is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();

        return await _context.Expenses
          .Include(m => m.Month)
          .Include(m => m.Category)// Ensure Category is loaded
          .Select(m => new ExpenseDto(
              m.Id,
              m.Name,
              m.Amount,
              m.Category.Name,
              m.Month.Name
              ))
          .ToListAsync();
    }

    public static async Task<int> AddExpenseAsync(IDbContextFactory<AppDbContext> _contextFactory, ExpenseRequest mr)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("MonthService is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();
        var mon = new Expense { Name = mr.Name, MonthId = mr.MonthId, Amount = mr.Amount, CategoryId = mr.CategoryId };
        _context.Expenses.Add(mon);
        await _context.SaveChangesAsync();
        return mon.Id;
    }
    public static async Task<List<IncomeDto>> GetAllIncomeAsync(IDbContextFactory<AppDbContext> _contextFactory)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("MonthService is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();

        return await _context.Incomes
     .Select(m => new IncomeDto(
         m.Id,
         m.Source,
         m.Amount,
         m.MonthId
     ))
     .ToListAsync();
    }

    public static async Task<int> AddIncomeAsync(IDbContextFactory<AppDbContext> _contextFactory, IncomeRequest mr)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("MonthService is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();
        var mon = new Income { Source = mr.Source, MonthId = mr.MonthId, Amount = mr.Amount };
        _context.Incomes.Add(mon);
        await _context.SaveChangesAsync();
        return mon.Id;
    }

    public static async Task<List<CatergoryDto>> GetAllCategoryAsync(IDbContextFactory<AppDbContext> _contextFactory)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("MonthService is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();

        return await _context.Categories// Ensure Category is loaded
     .Select(m => new CatergoryDto(
         m.Id,
         m.Name
     ))
     .ToListAsync();
    }
    public static async Task<List<MonthDto>> GetAllMonthsAsync(IDbContextFactory<AppDbContext> _contextFactory)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("MonthService is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();

        return await _context.Months
     .Include(m => m.Incomes)
     .Include(m => m.Expenses)
     .ThenInclude(e => e.Category) // Ensure Category is loaded
     .Select(m => new MonthDto(
         m.Id,
         m.Name,
         m.Year,
         m.Incomes.Select(i => new IncomeDto(i.Id, i.Source, i.Amount, i.MonthId)).ToList(),
         m.Expenses.Select(e => new ExpenseDto(e.Id, e.Name, e.Amount, e.Category.Name, e.Month.Name)).ToList()
     ))
     .ToListAsync();
    }

    public static async Task<int> AddMonthsAsync(IDbContextFactory<AppDbContext> _contextFactory, MonthRequest mr)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("MonthService is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();
        var mon = new Month { Name = mr.Name, Year = mr.Year };
        _context.Months.Add(mon);
        await _context.SaveChangesAsync();
        return mon.Id;
    }
    public static async Task<int> AddCategoryAsync(IDbContextFactory<AppDbContext> _contextFactory, CategoryRequest mr)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("MonthService is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();
        var mon = new Category { Name = mr.Name };
        _context.Categories.Add(mon);
        await _context.SaveChangesAsync();
        return mon.Id;
    }
}