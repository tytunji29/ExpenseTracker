namespace Infrastructure.Services;

public static class AllService
{
    public static async Task<ReturnObject> GetAllExpenseAsync(IDbContextFactory<AppDbContext> _contextFactory, int pgSz, int pgNumber)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("Budgetservice is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();
        var totalRecords = await _context.Expenses.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalRecords / pgSz);
        var expenses = await _context.Expenses
          .Include(m => m.Month)
          .Include(m => m.Category)// Ensure Category is loaded
          .Select(m => new ExpenseDto(
              m.Id,
              m.Name,
              m.Status,
              m.Amount,
              m.Category.Name,
              m.Month.Name,
              m.Month.Year
              ))
          .Skip(pgSz * (pgNumber - 1))
          .Take(pgSz)
          .ToListAsync();
        return new ReturnObject { status = true, data = new { Expenses = expenses, TotalPages = totalPages }, message = "Record Pulled Successfully" };
    }

    public static async Task<int> UpdateExpenseAsync(IDbContextFactory<AppDbContext> _contextFactory, int id, ExpenseRequestStatus mr)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("Budgetservice is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();
        var mon = _context.Expenses.FirstOrDefault(o => o.Id == id);
        mon.Status = mr.Status;
        await _context.SaveChangesAsync();
        return mon.Id;
    }
    public static async Task<int> AddExpenseAsync(IDbContextFactory<AppDbContext> _contextFactory, ExpenseRequest mr)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("Budgetservice is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();
        var mon = new Expense { Name = mr.Name, MonthId = mr.MonthId, Amount = mr.Amount, CategoryId = mr.CategoryId, Status = "Unpaid" };
        _context.Expenses.Add(mon);
        await _context.SaveChangesAsync();
        return mon.Id;
    }
    public static async Task<List<IncomeDto>> GetAllIncomeAsync(IDbContextFactory<AppDbContext> _contextFactory)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("Budgetservice is not initialized. Call Initialize() first.");

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
    public static async Task<List<MonthDto>> GetAllMonthAsync(IDbContextFactory<AppDbContext> _contextFactory)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("Monthservice is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();

        return await _context.Months
     .Select(m => new MonthDto(
         m.Id,
         m.Name,
         $"{m.Name} {m.Year}",
         m.Year
     ))
     .ToListAsync();
    }

    public static async Task<int> AddIncomeAsync(IDbContextFactory<AppDbContext> _contextFactory, IncomeRequest mr)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("Budgetservice is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();
        var mon = new Income { Source = mr.Source, MonthId = mr.MonthId, Amount = mr.Amount };
        _context.Incomes.Add(mon);
        await _context.SaveChangesAsync();
        return mon.Id;
    }

    public static async Task<List<CatergoryDto>> GetAllCategoryAsync(IDbContextFactory<AppDbContext> _contextFactory)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("Budgetservice is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();

        return await _context.Categories// Ensure Category is loaded
     .Select(m => new CatergoryDto(
         m.Id,
         m.Name
     ))
     .ToListAsync();
    }
    public static async Task<List<BudgetDto>> GetAllBudgetsAsync(IDbContextFactory<AppDbContext> _contextFactory)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("Budgetservice is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();

        return await _context.Months
     .Include(m => m.Incomes)
     .Include(m => m.Expenses)
     .ThenInclude(e => e.Category) // Ensure Category is loaded
     .Select(m => new BudgetDto(
         m.Id,
         m.Name,
         m.Year,
         m.Incomes.Select(i => new IncomeDto(i.Id, i.Source, i.Amount, i.MonthId)).ToList(),
         m.Expenses.Select(e => new ExpenseDto(e.Id, e.Name, e.Status, e.Amount, e.Category.Name, e.Month.Name, e.Month.Year)).ToList(),
         m.Incomes.Select(i => i.Amount).Sum(),
         m.Expenses.Select(i => i.Amount).Sum()
     ))
     .ToListAsync();
    }

    public static async Task<int> AddBudgetsAsync(IDbContextFactory<AppDbContext> _contextFactory, BudgetRequest mr)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("Budgetservice is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();
        var mon = new Month { Name = mr.Name, Year = mr.Year };
        _context.Months.Add(mon);
        await _context.SaveChangesAsync();
        return mon.Id;
    }
    public static async Task<int> UpdateMonthsAsync(IDbContextFactory<AppDbContext> _contextFactory, int id, BudgetRequest mr)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("Budgetservice is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();
        Month? formalMonths = _context.Months.FirstOrDefault(o => o.Id == id);
        if (formalMonths != null)
        {
            formalMonths.Name = mr.Name; formalMonths.Year = mr.Year;
        }
        int md = await _context.SaveChangesAsync();
        return md;
    }
    public static async Task<int> AddCategoryAsync(IDbContextFactory<AppDbContext> _contextFactory, CategoryRequest mr)
    {
        if (_contextFactory == null)
            throw new InvalidOperationException("Budgetservice is not initialized. Call Initialize() first.");

        using var _context = _contextFactory.CreateDbContext();
        var mon = new Category { Name = mr.Name };
        _context.Categories.Add(mon);
        await _context.SaveChangesAsync();
        return mon.Id;
    }
}