using System.Collections.Generic;
using System.Linq;
using Microcharts;
using Xamarin.Forms;

namespace HealthTracker.Skia
{
    public class SimpleLineChartBuilder
    {
        private readonly LineChart _lineChart;
        private readonly List<float> _values = new List<float>();

        public float Offset { get; set; } = 10;

        public float LineSize
        {
            get => _lineChart.LineSize;
            set => _lineChart.LineSize = value;
        }

        public float PointSize
        {
            get => _lineChart.PointSize;
            set => _lineChart.PointSize = value;
        }

        public LineMode LineMode
        {
            get => _lineChart.LineMode;
            set => _lineChart.LineMode = value;
        }

        public Color BackgroundColor
        {
            get => _lineChart.BackgroundColor.ToColor();
            set => _lineChart.BackgroundColor = value.ToSKColor();
        }

        public Color ForegroundColor { get; set; } = Color.DarkBlue;


        public SimpleLineChartBuilder()
        {
            _lineChart = new LineChart()
            {
                MinValue = 0,
                MaxValue = 100,
                LineSize = 10,
                PointSize = 20,
                LineMode = LineMode.Straight,
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
                return _lineChart;

            var min = _values.Min();
            var max = _values.Max();
            var factor = (100 - (2 * Offset)) / (max != min ? max - min : 2);

            _lineChart.Entries = _values.Select(v => BuildEntry(v, min, max, factor));

            return _lineChart;
        }

        private ChartEntry BuildEntry(float current, float min, float max, float factor)
        {
            var value = ((max != min ? current - min : 1) * factor) + Offset;
            return new ChartEntry(value)
            {
                Color = ForegroundColor.ToSKColor()
            };
        }
    }
}
