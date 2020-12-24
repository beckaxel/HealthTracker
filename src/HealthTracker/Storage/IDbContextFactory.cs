namespace HealthTracker.Storage
{
    public interface IDbContextFactory
    {
        HealthTrackerDbContext CreateHealthTrackerDbContext();
    }
}