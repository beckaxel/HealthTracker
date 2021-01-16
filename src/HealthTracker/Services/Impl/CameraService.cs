using System.IO;
using System.Threading.Tasks;

namespace HealthTracker.Services.Impl
{
    public class CameraService : ICameraService
    {
        private class TakePhotoResult : IPhoto
        {
            public TakePhotoResult(string filePath, byte[] content)
            {
                FileName = Path.GetFileName(filePath);
                OriginDirectoryName = Path.GetDirectoryName(filePath);
                Content = content;
            }

            public string FileName { get; }

            public byte[] Content { get; }

            public string OriginDirectoryName { get; }
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
            
            //var bytes = await File.ReadAllBytesAsync(path);
            //File.Delete(path);

            return new TakePhotoResult(path, null);
        }
    }
}
