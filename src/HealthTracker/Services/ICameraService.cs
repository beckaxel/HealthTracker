using System.Threading.Tasks;

namespace HealthTracker.Services
{
    public interface ICameraService
    {
        Task<IPhoto> TakePhotoAsync();
    }
}
