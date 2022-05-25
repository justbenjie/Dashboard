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
            Thread.Sleep(6000);
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
                vacanciesInfo = await ClientHTTP.GetVacanciesInfoAsync(vacancyName, host);
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

        private void buttonPowerPoint_Click(object sender, EventArgs e)
        {
            // Open PowerPoint presentation
            buttonPowerPoint.Enabled = false;
            PowerPoint.Show(outputsDirectory + "Анализ вакансий с HeadHunter.pptx");
            buttonPowerPoint.Enabled = true;
        }

        private void buttonWord_Click(object sender, EventArgs e)
        {
            // Load some vacancies info to word file and open it 
            buttonWord.Enabled = false;

            // Save salary chart to png
            Bitmap bitmap = new Bitmap(chartSalary.Size.Width, chartSalary.Size.Height);    
            chartSalary.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height));
            bitmap.Save(outputsDirectory + "salaryChart.png", ImageFormat.Png);

            Word wordCreator = new Word();
            
            // Add text information 
            wordCreator.AddParagraph(vacancyName.Text, WdParagraphAlignment.wdAlignParagraphCenter);
            wordCreator.Enter();

            wordCreator.AddParagraph("Number of vacancies:", WdParagraphAlignment.wdAlignParagraphCenter);
            wordCreator.AddText(vacanciesInfo.Count.ToString(), WdParagraphAlignment.wdAlignParagraphLeft);
            wordCreator.Enter();

            wordCreator.AddParagraph("Skills:", WdParagraphAlignment.wdAlignParagraphCenter);
            foreach(var item in vacanciesInfo.Skills.Keys)
            {
                wordCreator.AddText(item, WdParagraphAlignment.wdAlignParagraphLeft);
            }
            wordCreator.Enter();

            wordCreator.AddParagraph("Salary (min, median, max):", WdParagraphAlignment.wdAlignParagraphCenter);
            wordCreator.AddText(vacanciesInfo.Salary.SalaryMin.ToString(), WdParagraphAlignment.wdAlignParagraphLeft);
            wordCreator.AddText(vacanciesInfo.Salary.SalaryMedian.ToString(), WdParagraphAlignment.wdAlignParagraphLeft);
            wordCreator.AddText(vacanciesInfo.Salary.SalaryMedian.ToString(), WdParagraphAlignment.wdAlignParagraphLeft);
            wordCreator.Enter();

            // Add salary chart 
            wordCreator.LoadImage(outputsDirectory + "salaryChart.png", WdParagraphAlignment.wdAlignParagraphCenter);
            wordCreator.Visible = true;
            wordCreator.Dispose();

            buttonWord.Enabled = true;
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            string name = "О программе";
            string description = "Приложение для анализа вакансий с HeadHunter. v.1.0\nРазработано Нестеровым Ф.С.";

            MessageBox.Show(description, name);
        }

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            // Open Excel with parsed from by server data from HH API
            buttonExcel.Enabled = false;
            Excel.Show(outputsDirectory + "../../../HH_analysis/logging.xlsx");
            buttonExcel.Enabled = true;
        }

        private void buttonManual_Click(object sender, EventArgs e)
        {
            // Open manual (helpfile)
            buttonHelp.Enabled = false;
            Help.ShowHelp(this, outputsDirectory + "helpFile.chm");
            buttonHelp.Enabled = true;
        }

       
    }
}
