using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using ApiClient;
using Charts;
using OutputMicrosoft;

namespace Dashboard
{
    public partial class MainForm : Form
    {
        private PieChart scheduleChart; 
        private PieChart experienceChart;
        private ColumnChart salaryChart;
        private RowChart skillsChart;
        private VacanciesInfo vacanciesInfo; // For received from server information about vacancies
        private SettingData data; // For cached setting parameters
        private string outputsDirectory = Path.Combine(Directory.GetCurrentDirectory(),@"../../Output/"); // Directory with outputs

        public MainForm()
        {
            // Create new Thread for run splash screen 
            Thread thread = new Thread(new ThreadStart(StartForm));
            thread.Start();
            InitializeComponent();
            data = new SettingData();

            // Load data from xml file
            if (File.Exists("settings.xml"))
            {
                data = XMLCreator.ReadData(data, "settings.xml");
                vacancyName.Text = data.VacancyName;
                host.Text = data.Host;
            }
            
            UpdateForm();
            Thread.Sleep(4000);
            thread.Abort();
        }

        private void StartForm()
        {
            // Run splash screen form
            System.Windows.Forms.Application.Run(new SplashScreen());
        }

        private async void UpdateForm()
        {
            buttonUpdate.Enabled = false;
            label17.Text = "загрузка...";

            // Save data from texboxes to xml file
            string vacancyName = this.vacancyName.Text.ToString();
            string host = this.host.Text.ToString();
            try
            {
                data.VacancyName = vacancyName;
                data.Host = host;

                XMLCreator.SavaData(data, @"settings.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // Async get vacancies from server
            try
            {
                vacanciesInfo = await ClientTCP.GetVacanciesInfoAsync(vacancyName, host);
                UpdateLabels(vacanciesInfo);
                UpdateCharts(vacanciesInfo);
                label17.Text = "Данные успешно получены!";
            }

            catch (Exception e)
            {
                label17.Text = "Связь с сервером не установлена!";
            }
            
            buttonUpdate.Enabled = true;

        }

        private void UpdateLabels(VacanciesInfo vacanciesInfo)
        {
            // Update labels
            label3.Text = vacanciesInfo.Count.ToString();
            label9.Text = vacanciesInfo.Salary.SalaryMin.ToString() + " $";
            label8.Text = vacanciesInfo.Salary.SalaryMax.ToString() + " $";
            label10.Text = vacanciesInfo.Salary.SalaryMedian.ToString() + " $";
        }

        private void UpdateCharts(VacanciesInfo vacanciesInfo)
        {
            // Update charts 
            experienceChart = new PieChart(chartWorkingExp);
            experienceChart.DrawPiechart(vacanciesInfo.Experience);

            scheduleChart = new PieChart(chartSchedule);
            scheduleChart.DrawPiechart(vacanciesInfo.Schedule);

            skillsChart = new RowChart(chartSkills);
            skillsChart.DrawRowchart(vacanciesInfo.Skills);

            salaryChart = new ColumnChart(chartSalary);
            salaryChart.DrawColumnchart(vacanciesInfo.Salary.SalaryExpMedian);
        }

        private void update_Click(object sender, EventArgs e)
        {
            UpdateForm();
        }

       
       
    }
}
