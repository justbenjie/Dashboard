using ApiClient;
using ColumnCharts;
using PieCharts;
using RowCharts;
using System;
using System.Windows.Forms;

namespace Dashboard
{
    public partial class MainForm : Form
    {
        private Piechart pieChart;
        private Columnchart columnChart;
        private Rowchart rowChart;
        private VacanciesInfo vacanciesInfo;
        

        public MainForm()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            update.Enabled = false;
            label17.Text = "loading...";

            string vacancyName = textBox1.Text.ToString();
            vacanciesInfo = await Client.GetVacanciesInfoAsync(vacancyName);
            if (vacanciesInfo == null)
            {
                label17.Text = "Can't connect to server!";
            }
            else
            {   
                UpdateLabels(vacanciesInfo);
                UpdateCharts(vacanciesInfo);
                label17.Text = "loaded successfully!";
            }
            update.Enabled = true;
            
        }

        private void UpdateLabels(VacanciesInfo vacanciesInfo)
        {
            label3.Text = vacanciesInfo.Count.ToString();
            label9.Text = vacanciesInfo.Salary.SalaryMin.ToString() + " $";
            label8.Text = vacanciesInfo.Salary.SalaryMax.ToString() + " $";
            label10.Text = vacanciesInfo.Salary.SalaryMedian.ToString() + " $";
        }

        private void UpdateCharts(VacanciesInfo vacanciesInfo)
        {
            pieChart = new Piechart(workingExp);
            pieChart.DrawPiechart(vacanciesInfo.Experience);

            pieChart = new Piechart(schedule);
            pieChart.DrawPiechart(vacanciesInfo.Schedule);

            rowChart = new Rowchart(skills);
            rowChart.DrawRowchart(vacanciesInfo.Skills);

            columnChart = new Columnchart(salary);
            columnChart.DrawColumnchart(vacanciesInfo.Salary.SalaryExpMedian);
            

        }
        
    }
}
