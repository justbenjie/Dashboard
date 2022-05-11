using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;

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

        public void DrawRowchart(Dictionary<string, double> value_count)
        {

            foreach (var item in value_count)
            {
                labels.Add(item.Key);
                values.Add(item.Value);
            };

            RowSeries rowSeries = new RowSeries
            {
                Title = "Frequency",
                Values = values,
                DataLabels = true,
                
            };
            chart.Series.Add(rowSeries);

            chart.AxisX.Add(new Axis
            {
                Title = "Frequency",
            });

            chart.AxisY.Add(new Axis
            {
                Title = "Skills",
                Labels = labels
            });

        }
    }
}
