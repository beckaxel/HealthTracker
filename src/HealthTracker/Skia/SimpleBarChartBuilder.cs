using System.Collections.Generic;
using System.Linq;
using Microcharts;
using Xamarin.Forms;

namespace HealthTracker.Skia
{
    public class SimpleBarChartBuilder
    {
        private readonly BarChart _barChart;
        private readonly List<float> _values = new List<float>();

        public float? MaxValue { get; set; }

        public float? MinValue { get; set; }

        public Color BackgroundColor
        {
            get => _barChart.BackgroundColor.ToColor();
            set => _barChart.BackgroundColor = value.ToSKColor();
        }

        public Color ForegroundColor { get; set; } = Color.DarkBlue;

        public SimpleBarChartBuilder()
        {
            _barChart = new BarChart()
            {
                MinValue = 0,
                MaxValue = 100,
                PointSize = 0,
                BackgroundColor = Color.CornflowerBlue.ToSKColor()
            };
        }

        public void AddValue(float value)
        {
            _values.Add(value);
        }

        public void AddValueRange(IEnumerable<float> values)
        {
            _values.AddRange(values);
        }

        public Chart ToChart()
        {
            if (_values.Count == 0)
                return _barChart;

            var min = MinValue ??  _values.Min();
            var max = MaxValue ?? _values.Max();
            var factor = 100 / (max != min ? max - min : 2);

            _barChart.Entries = _values.Select(v => BuildEntry(v, min, max, factor));

            return _barChart;
        }

        private ChartEntry BuildEntry(float current, float min, float max, float factor)
        {
            var value = (max != min ? current - min : 1) * factor;
            return new ChartEntry(value)
            {
                Color = ForegroundColor.ToSKColor()
            };
        }
    }
}
