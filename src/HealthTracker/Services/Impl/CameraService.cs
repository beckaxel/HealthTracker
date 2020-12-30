using System.IO;
using System.Threading.Tasks;

namespace HealthTracker.Services.Impl
{
    public class CameraService : ICameraService
    {
        private class TakePhotoResult : IPhoto
        {
            public TakePhotoResult(string fileName, byte[] content)
            {
                FileName = fileName;
                Content = content;
            }

            public string FileName { get; }

            public byte[] Content { get; }
        }

        public async Task<IPhoto> TakePhotoAsync()
        {
            string path;
            using (var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()))
            {
                path = photo?.Path;
            }
            if (path == null)
                return null;

            var fileName = Path.GetFileName(path);
            var bytes = await File.ReadAllBytesAsync(path);

            File.Delete(path);
            return new TakePhotoResult(fileName, bytes);
        }
    }
}
