using System.Collections.Generic;
using Xamarin.Forms;

namespace HealthTracker.MVVM
{
    public abstract partial class SectionMainView : ViewBase
    {
        public SectionMainView()
        {
            InitializeComponent();
        }
        
        #region SectionName

        private static BindableProperty SectionNameProperty = BindableProperty.Create(nameof(SectionName), typeof(string), typeof(SectionMainView));

        public string SectionName
        {
            get => (string)GetValue(SectionNameProperty);
            set => SetValue(SectionNameProperty, value);
        }

        #endregion

        #region SectionIconImageSource

        private static BindableProperty SectionIconImageSourceProperty = BindableProperty.Create(nameof(SectionIconImageSource), typeof(ImageSource), typeof(SectionMainView));

        public ImageSource SectionIconImageSource
        {
            get => (ImageSource)GetValue(SectionIconImageSourceProperty);
            set => SetValue(SectionIconImageSourceProperty, value);
        }

        #endregion

        #region SectionCollation

        private static BindableProperty SectionCollationProperty = BindableProperty.Create(nameof(SectionCollation), typeof(int), typeof(SectionMainView));

        public int SectionCollation
        {
            get => (int)GetValue(SectionCollationProperty);
            set => SetValue(SectionCollationProperty, value);
        }

        #endregion

        #region SectionSummary

        private static BindableProperty SectionSummaryProperty = BindableProperty.Create(nameof(SectionSummary), typeof(View), typeof(SectionMainView));

        public View SectionSummary
        {
            get => (View)GetValue(SectionSummaryProperty);
            set => SetValue(SectionSummaryProperty, value);
        }

        #endregion

        #region SectionFilters
        
        private static BindableProperty SectionFiltersProperty = BindableProperty.Create(nameof(SectionFilters), typeof(FilterCollection), typeof(SectionMainView));

        public FilterCollection SectionFilters
        {
            get => (FilterCollection)GetValue(SectionFiltersProperty);
            set => SetValue(SectionFiltersProperty, value);
        }
        
        #endregion

        #region SectionContent

        private static BindableProperty SectionContentProperty = BindableProperty.Create(nameof(SectionContent), typeof(View), typeof(SectionMainView));
        
        public View SectionContent
        {
            get => (View)GetValue(SectionContentProperty);
            set => SetValue(SectionContentProperty, value);
        }

        #endregion
    }
}
