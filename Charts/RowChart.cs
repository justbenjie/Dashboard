using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;
using System.Windows.Media;

namespace Charts
{
    public class RowChart
    {
        private LiveCharts.WinForms.CartesianChart chart;
        private IList<string> labels = new List<string>() { };
        private ChartValues<double> values = new ChartValues<double> { };

        public RowChart(LiveCharts.WinForms.CartesianChart chart)
        {
            this.chart = chart;
        }

        public void DrawRowchart(Dictionary<string, int> value_count)
        {
            // Clear chart
            chart.Series.Clear();
            chart.AxisX.Clear();
            chart.AxisY.Clear();

            // Parse dictionary
            foreach (var item in value_count.Reverse())
            {
                labels.Add(item.Key);
                values.Add(item.Value);
            };

            // Add new Series
            RowSeries rowSeries = new RowSeries
            {
                Title = "Частота",
                Values = values,
                DataLabels = true,
                Fill = Brushes.Red,
                Foreground = Brushes.WhiteSmoke,
            };
            chart.Series.Add(rowSeries);

            // Axis X
            chart.AxisX.Add(new Axis
            {
                Title = "Частота",
                Foreground = Brushes.WhiteSmoke
            });

            // Axis Y
            chart.AxisY.Add(new Axis
            {
                Title = "Навыки",
                Labels = labels,
                Foreground = Brushes.WhiteSmoke
            });

        }
    }
}
