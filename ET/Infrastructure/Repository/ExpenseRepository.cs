namespace Infrastructure.Repository;

public interface IExpenseRepository
{
    Task<ReturnObject> AddExpense(ExpenseRequest request);
    Task<ReturnObject> UpdateExpenseStatus(int id, ExpenseRequestStatus request);
    Task<ReturnObject> GetAllExpense(int pgSize, int pgNumber);
}
public class ExpenseRepository : IExpenseRepository
{
    private static IDbContextFactory<AppDbContext> _contextFactory;
    public ExpenseRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<ReturnObject> GetAllExpense(int pgSize, int pgNumber)
    {
        var res = await AllService.GetAllExpenseAsync(_contextFactory, pgSize, pgNumber);
        return res;
    }
    public async Task<ReturnObject> UpdateExpenseStatus(int id, ExpenseRequestStatus request)
    {
        var res = await AllService.UpdateExpenseAsync(_contextFactory, id, request);
        if (res > 0)
            return new ReturnObject { status = true, message = "Record Updated Successfully", data = res };
        return new ReturnObject { status = false, message = "An Error Occured", data = null };
    }
    public async Task<ReturnObject> AddExpense(ExpenseRequest request)
    {
        var res = await AllService.AddExpenseAsync(_contextFactory, request);
        if (res > 0)
            return new ReturnObject { status = true, message = "Record Added Successfully", data = res };
        return new ReturnObject { status = false, message = "An Error Occured", data = null };
    }
}
