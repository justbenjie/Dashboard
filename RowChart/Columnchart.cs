using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;

namespace ColumnCharts
{
    public class Columnchart
    {
        LiveCharts.WinForms.CartesianChart chart;
        public ChartValues<double> values = new ChartValues<double> { };
        public IList<string> labels = new List<string>() { };

        public Columnchart(LiveCharts.WinForms.CartesianChart chart)
        {
            this.chart = chart;
        }

        public void DrawColumnchart(Dictionary<string, double> value_count)
        {
            
            foreach (var item in value_count)
            {
                labels.Add(item.Key);
                values.Add(item.Value);
            };

            ColumnSeries columnSeries = new ColumnSeries
            {
                Title = "Median",
                Values = values,
                DataLabels = true,
       
            };
            chart.Series.Add(columnSeries);

            chart.AxisX.Add(new Axis
            {
                Title = "Experience",
                Labels = labels
            });

            chart.AxisY.Add(new Axis
            {
                Title = "Median salary",
            });
        }
    }
}
