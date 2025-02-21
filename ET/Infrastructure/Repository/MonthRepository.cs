namespace Infrastructure.Repository;

public interface IMonthRepository
{
    Task<ReturnObject> AddMonth(MonthRequest request);
    Task<ReturnObject> GetAllMonth();
}
public class MonthRepository : IMonthRepository
{
    private static IDbContextFactory<AppDbContext> _contextFactory;
    public MonthRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<ReturnObject> GetAllMonth()
    {
        var res = await AllService.GetAllMonthsAsync(_contextFactory);
        return new ReturnObject { status = true, message = "Record Fund Successfully", data = res };
    }
    public async Task<ReturnObject> AddMonth(MonthRequest request)
    {
        var res = await AllService.AddMonthsAsync(_contextFactory, request);
        if (res > 0)
            return new ReturnObject { status = true, message = "Record Added Successfully", data = res };
        return new ReturnObject { status = false, message = "An Error Occured", data = null };
    }
}
