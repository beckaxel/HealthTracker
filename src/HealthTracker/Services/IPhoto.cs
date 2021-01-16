namespace HealthTracker.Services
{
    public interface IPhoto
    {
        string OriginDirectoryName { get; }
        string FileName { get; }
        byte[] Content { get; }
    }
}
