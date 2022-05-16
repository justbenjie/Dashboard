using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;
using System.Windows.Media;
using System.Windows;

namespace PieCharts
{
    public class Piechart
    {
        LiveCharts.WinForms.PieChart chart;
        public Func<ChartPoint, string> labelPoint = chartPoint =>
            string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

        public Piechart(LiveCharts.WinForms.PieChart chart)
        {
            this.chart = chart;
        }

        public void DrawPiechart(Dictionary<string, int> value_count)
        {
            chart.Series.Clear();
            chart.AxisX.Clear();
            chart.AxisY.Clear();
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
                    StrokeThickness = 0
                };
                chart.Series.Add(pieSeries);
            }

            DefaultLegend customLegend = new DefaultLegend();
            customLegend.BulletSize = 14;
            customLegend.Foreground = Brushes.WhiteSmoke;
            customLegend.Orientation = System.Windows.Controls.Orientation.Horizontal;
            chart.DefaultLegend = customLegend;
            chart.LegendLocation = LegendLocation.Bottom;
            
           
            
           
        }
    }
}
