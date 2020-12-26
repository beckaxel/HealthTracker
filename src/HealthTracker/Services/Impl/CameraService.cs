using System.IO;
using System.Threading.Tasks;

namespace HealthTracker.Services.Impl
{
    public class CameraService : ICameraService
    {
        public async Task<Stream> TakePhotoAsync()
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
            return photo?.GetStream();
        }
    }
}
