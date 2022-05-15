using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;
using System.Windows.Media;

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
                    Stroke = Brushes.DarkGray,

                };
                chart.Series.Add(pieSeries);
            }

            chart.LegendLocation = LegendLocation.Bottom;
            

        }
    }
}
