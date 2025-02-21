namespace Infrastructure.Repository;

public interface IExpenseRepository
{
    Task<ReturnObject> AddExpense(ExpenseRequest request);
    Task<ReturnObject> GetAllExpense();
}
public class ExpenseRepository : IExpenseRepository
{
    private static IDbContextFactory<AppDbContext> _contextFactory;
    public ExpenseRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<ReturnObject> GetAllExpense()
    {
        var res = await AllService.GetAllExpenseAsync(_contextFactory);
        return new ReturnObject { status = true, message = "Record Fund Successfully", data = res };
    }
    public async Task<ReturnObject> AddExpense(ExpenseRequest request)
    {
        var res = await AllService.AddExpenseAsync(_contextFactory, request);
        if (res > 0)
            return new ReturnObject { status = true, message = "Record Added Successfully", data = res };
        return new ReturnObject { status = false, message = "An Error Occured", data = null };
    }
}
