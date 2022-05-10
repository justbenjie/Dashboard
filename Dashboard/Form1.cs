using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts; //Core of the library
using LiveCharts.Wpf; //The WPF controls
using LiveCharts.WinForms; //the WinForm wrappers
using PieCharts;
using RowCharts;

namespace Dashboard
{
    public partial class Form1 : Form
    {
        private Piechart pieChart;
        private Rowchart rowChart;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pieChart = new Piechart(pieChart1.Series);
            pieChart.DrawPiechart(new Dictionary<string, int> { { "1-3 years", 6 }, { "3-6 years", 12 } });

            pieChart1.LegendLocation = LegendLocation.Bottom;

            pieChart = new Piechart(pieChart2.Series);
            pieChart.DrawPiechart(new Dictionary<string, int> { { "Remote", 2 }, { "Offline", 5 } });

            pieChart2.LegendLocation = LegendLocation.Bottom;

            rowChart = new Rowchart(cartesianChart2.Series);
            
            rowChart.DrawRowchart(new Dictionary<string, int> { { "Python", 15 }, { "SQL", 10 }, { "Java", 5 } });
            cartesianChart2.LegendLocation = LegendLocation.Left;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pieChart2_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pieChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}
