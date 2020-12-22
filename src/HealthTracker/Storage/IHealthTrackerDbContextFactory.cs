namespace HealthTracker.Storage
{
    public interface IHealthTrackerDbContextFactory
    {
        HealthTrackerDbContext Create();
    }
}