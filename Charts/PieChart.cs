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
    public class PieChart
    {
        private LiveCharts.WinForms.PieChart chart;
        private Func<ChartPoint, string> labelPoint = chartPoint =>
            string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

        public PieChart(LiveCharts.WinForms.PieChart chart)
        {
            this.chart = chart;
        }

        public void DrawPiechart(Dictionary<string, int> value_count)
        {
            // Clear chart
            chart.Series.Clear();
            chart.AxisX.Clear();
            chart.AxisY.Clear();
            this.chart.Update(true, true);

            // Adding values to piechart
            foreach (var item in value_count)
            {
                PieSeries pieSeries = new PieSeries
                {
                    Title = item.Key,
                    Values = new ChartValues<int> { item.Value },
                    DataLabels = true,
                    LabelPoint = labelPoint,
                    Stroke = Brushes.WhiteSmoke,
                    Foreground = Brushes.WhiteSmoke,
                    StrokeThickness = 0,

                };
                chart.Series.Add(pieSeries);
            }

            // Legend
            DefaultLegend customLegend = new DefaultLegend();
            customLegend.BulletSize = 14;
            customLegend.Foreground = Brushes.WhiteSmoke;
            customLegend.Orientation = System.Windows.Controls.Orientation.Horizontal;
            chart.DefaultLegend = customLegend;
            chart.LegendLocation = LegendLocation.Bottom;
        }
    }
}
