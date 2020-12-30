using System;
using HealthTracker.Models;
using HealthTracker.MVVM;

namespace HealthTracker.ViewModels
{
    public class PhotoViewModel : ViewModelBase
    {
        protected Photo Photo;

        #region Parameter Handling

        protected override void OnParameterChanged(object oldValue, object newValue)
        {
            if (newValue == MVVM.Parameter.Empty)
            {
                Photo = new Photo();
                MapFrom(Photo);
            }
            else if (newValue is Photo photo)
            {
                Photo = photo;
                MapFrom(Photo);
            }
        }

        #endregion

        #region FileName

        private string _fileName;

        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        #endregion

        #region RecordingTime

        private DateTime _recordingTime;

        public DateTime RecordingTime
        {
            get => _recordingTime;
            set => SetProperty(ref _recordingTime, value);
        }

        #endregion

        #region Content

        private byte[] _content;

        public byte[] Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        #endregion
    }
}
