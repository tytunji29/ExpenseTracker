namespace Infrastructure.Repository;

public interface IBudgetRepository
{
    Task<ReturnObject> AddBudget(BudgetRequest request);
    Task<ReturnObject> GetAllBudget();
    Task<ReturnObject> GetAllMonth();
    Task<ReturnObject> UpdateMonth(int id, BudgetRequest request);
}
public class BudgetRepository : IBudgetRepository
{
    private static IDbContextFactory<AppDbContext> _contextFactory;
    public BudgetRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<ReturnObject> GetAllBudget()
    {
        var res = await AllService.GetAllBudgetsAsync(_contextFactory);
        return new ReturnObject { status = true, message = "Record Fund Successfully", data = res };
    }
    public async Task<ReturnObject> GetAllMonth()
    {
        var res = await AllService.GetAllMonthAsync(_contextFactory);
        return new ReturnObject { status = true, message = "Record Fund Successfully", data = res };
    }
    public async Task<ReturnObject> AddBudget(BudgetRequest request)
    {
        var res = await AllService.AddBudgetsAsync(_contextFactory, request);
        if (res > 0)
            return new ReturnObject { status = true, message = "Record Added Successfully", data = res };
        return new ReturnObject { status = false, message = "An Error Occured", data = null };
    }
    public async Task<ReturnObject> UpdateMonth(int id, BudgetRequest request)
    {
        var res = await AllService.UpdateMonthsAsync(_contextFactory, id, request);
        if (res > 0)
            return new ReturnObject { status = true, message = "Record Updated Successfully", data = res };
        return new ReturnObject { status = false, message = "An Error Occured", data = null };
    }
}
