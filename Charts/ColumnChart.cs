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
    public class ColumnChart
    {
        private LiveCharts.WinForms.CartesianChart chart;
        private ChartValues<double> values = new ChartValues<double> { };
        private IList<string> labels = new List<string>() { };

        public ColumnChart(LiveCharts.WinForms.CartesianChart chart)
        {
            this.chart = chart;
        }

        // Add series
        public void AddSeries(string title, Dictionary<string, double> statistic, Brush color)
        {
            ChartValues<double> values = new ChartValues<double>();
            foreach (var item in statistic)
            {
                values.Add(item.Value);
                labels.Add(item.Key);
            };

            ColumnSeries columnSeries = new ColumnSeries
            {
                Title = title,
                Values = values,
                DataLabels = true,
                Fill = color,
                Foreground = Brushes.WhiteSmoke,
            };
            chart.Series.Add(columnSeries);

        }

        public void DrawColumnchart(Dictionary<string, double> salaryExp)
        {
            // Clear chart
            chart.Series.Clear();
            chart.AxisX.Clear();
            chart.AxisY.Clear();
            labels.Clear();

            AddSeries("Медиана", salaryExp, Brushes.DarkBlue);
            
            // Axis X
            chart.AxisX.Add(new Axis
            {
                Title = "Опыт работы",
                Labels = labels,
                Foreground = Brushes.WhiteSmoke

            });

            // Axis Y
            chart.AxisY.Add(new Axis
            {
                Title = "Зарплата (доллары)",
                Foreground = Brushes.WhiteSmoke
            });
        }
    }
}
