using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;
using System.Windows.Media;

namespace RowCharts
{
    public class Rowchart
    {
        LiveCharts.WinForms.CartesianChart chart;
        public IList<string> labels = new List<string>() { };
        public ChartValues<double> values = new ChartValues<double> { };

        public Rowchart(LiveCharts.WinForms.CartesianChart chart)
        {
            this.chart = chart;
        }

        public void DrawRowchart(Dictionary<string, int> value_count)
        {
            chart.Series.Clear();
            chart.AxisX.Clear();
            chart.AxisY.Clear();    
            foreach (var item in value_count.Reverse())
            {
                labels.Add(item.Key);
                values.Add(item.Value);
            };

            RowSeries rowSeries = new RowSeries
            {
                Title = "Frequency",
                Values = values,
                DataLabels = true,
                Fill = Brushes.DarkRed,
                Foreground = Brushes.WhiteSmoke,
            };
            chart.Series.Add(rowSeries);

            chart.AxisX.Add(new Axis
            {
                Title = "Frequency",
                Foreground = Brushes.WhiteSmoke
            });

            chart.AxisY.Add(new Axis
            {
                Title = "Skills",
                Labels = labels,
                Foreground = Brushes.WhiteSmoke
            });

        }
    }
}
