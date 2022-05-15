using ApiClient;
using ColumnCharts;
using PieCharts;
using RowCharts;
using System;
using System.Windows.Forms;

namespace Dashboard
{
    public partial class Form1 : Form
    {
        private Piechart pieChart;
        private Columnchart columnChart;
        private Rowchart rowChart;
        private VacanciesInfo vacanciesInfo;
        

        public Form1()
        {
            InitializeComponent();

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string vacancyName = textBox1.Text.ToString();

            vacanciesInfo = Client.GetVacanciesInfo(vacancyName);

            UpdateLabels(vacanciesInfo);

            pieChart = new Piechart(workingExp);
            pieChart.DrawPiechart(vacanciesInfo.Experience);

            pieChart = new Piechart(schedule);
            pieChart.DrawPiechart(vacanciesInfo.Schedule);

            rowChart = new Rowchart(skills);
            rowChart.DrawRowchart(vacanciesInfo.Skills);

            columnChart = new Columnchart(salary);
            columnChart.DrawColumnchart(vacanciesInfo.SalaryExp);
        }

        private void UpdateLabels(VacanciesInfo vacanciesInfo)
        {
            label3.Text = vacanciesInfo.Count.ToString();
            label9.Text = vacanciesInfo.SalaryMin.ToString() + " $";
            label8.Text = vacanciesInfo.SalaryMax.ToString() + " $";
            label10.Text = vacanciesInfo.SalaryMedian.ToString() + " $";
        }
        
    }
}
