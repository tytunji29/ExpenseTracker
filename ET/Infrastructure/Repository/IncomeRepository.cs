namespace Infrastructure.Repository;

public interface IIncomeRepository
{
    Task<ReturnObject> AddIncome(IncomeRequest request);
    Task<ReturnObject> GetAllIncome();
}
public class IncomeRepository : IIncomeRepository
{
    private static IDbContextFactory<AppDbContext> _contextFactory;
    public IncomeRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<ReturnObject> GetAllIncome()
    {
        var res = await AllService.GetAllIncomeAsync(_contextFactory);
        return new ReturnObject { status = true, message = "Record Fund Successfully", data = res };
    }
    public async Task<ReturnObject> AddIncome(IncomeRequest request)
    {
        var res = await AllService.AddIncomeAsync(_contextFactory, request);
        if (res > 0)
            return new ReturnObject { status = true, message = "Record Added Successfully", data = res };
        return new ReturnObject { status = false, message = "An Error Occured", data = null };
    }
}
