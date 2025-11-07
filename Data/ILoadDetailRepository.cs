public interface ILoadDetailRepository : IRepository<LoadDetail>
{
    // Add custom methods here if needed
}

public class LoadDetailRepository : Repository<LoadDetail>, ILoadDetailRepository
{
    public LoadDetailRepository(AppDbContext context) : base(context) { }

    // Add custom queries here if needed
}