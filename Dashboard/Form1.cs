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
using ColumnCharts;
using RowCharts;

namespace Dashboard
{
    public partial class Form1 : Form
    {
        private Piechart pieChart;
        private Columnchart columnChart;
        private Rowchart rowChart;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pieChart = new Piechart(workingExp);
            pieChart.DrawPiechart(new Dictionary<string, int> { { "1-3 years", 6 }, { "3-6 years", 12 } });

            pieChart = new Piechart(schedule);
            pieChart.DrawPiechart(new Dictionary<string, int> { { "Remote", 2 }, { "Offline", 5 } });

            rowChart = new Rowchart(skills);
            rowChart.DrawRowchart(new Dictionary<string, double> { { "Django", 7 }, { "Sql", 5 }, { "Python", 2 } });
          
            columnChart = new Columnchart(salary);
            columnChart.DrawColumnchart(new Dictionary<string, double> { { "1-3 years", 460 }, { "3-6 years", 2000 } });
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
