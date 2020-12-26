using System.IO;
using System.Threading.Tasks;

namespace HealthTracker.Services
{
    public interface ICameraService
    {
        Task<Stream> TakePhotoAsync();
    }
}
