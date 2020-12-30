namespace HealthTracker.Services
{
    public interface IPhoto
    {
        string FileName { get; }
        byte[] Content { get; }
    }
}
