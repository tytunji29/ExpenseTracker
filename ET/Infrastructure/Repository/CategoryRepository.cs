namespace Infrastructure.Repository;

public interface ICategoryRepository
{
    Task<ReturnObject> AddCategory(CategoryRequest request);
    Task<ReturnObject> GetAllCategory();
}
public class CategoryRepository : ICategoryRepository
{
    private static IDbContextFactory<AppDbContext> _contextFactory;
    public CategoryRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<ReturnObject> GetAllCategory()
    {
        var res = await AllService.GetAllCategoryAsync(_contextFactory);
        return new ReturnObject { status = true, message = "Record Fund Successfully", data = res };
    }
    public async Task<ReturnObject> AddCategory(CategoryRequest request)
    {
        var res = await AllService.AddCategoryAsync(_contextFactory, request);
        if (res > 0)
            return new ReturnObject { status = true, message = "Record Added Successfully", data = res };
        return new ReturnObject { status = false, message = "An Error Occured", data = null };
    }
}
