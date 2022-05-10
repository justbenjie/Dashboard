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
        public SeriesCollection series;

        public Rowchart(SeriesCollection series)
        {
            this.series = series;
        }

        public List<string> DrawRowchart(Dictionary<string, int> value_count)
        {
            series.Clear();
            List<string> labels = new List<string> { };
            foreach (var item in value_count)
            {
                labels.Append(item.Key);
                RowSeries rowSeries = new RowSeries
                {
                    Title = item.Key,
                    Values = new ChartValues<int> { item.Value },
                    DataLabels = true,
                };
                series.Add(rowSeries);
            }
            return labels;
        }
    }
}
