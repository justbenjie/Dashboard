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
        private VacanciesInfo vacanciesInfo;
        private SettingData data;

        public MainForm()
        {
            Thread thread = new Thread(new ThreadStart(StartForm));
            thread.Start();
            InitializeComponent();
            data = new SettingData();
            if (File.Exists("settings.xml"))
            {
                data = XMLCreator.ReadData(data, "settings.xml");
                vacancyName.Text = data.vacancyName;
            }
            UpdateForm();
            Thread.Sleep(5000);
            thread.Abort();
        }

        private void StartForm()
        {
            System.Windows.Forms.Application.Run(new SplashScreen());
        }

        private async void UpdateForm()
        {
            buttonUpdate.Enabled = false;
            label17.Text = "loading...";

            string vacancyName = this.vacancyName.Text.ToString();
            try
            {
                
                data.vacancyName = vacancyName;
                XMLCreator.SavaData(data, @"settings.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
            buttonUpdate.Enabled = true;

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
            buttonPowerPoint.Enabled = false;
            PowerPoint.Show(@"D:\БНТУ курс 2\РПВС\2 семестр\Dashboard\Dashboard\Output\Анализ вакансий с HeadHunter.pptx");
            buttonPowerPoint.Enabled = true;
        }

        private void buttonWord_Click(object sender, EventArgs e)
        {
            buttonWord.Enabled = false;

            Bitmap bitmap = new Bitmap(chartSalary.Size.Width, chartSalary.Size.Height);    
            chartSalary.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height));
            bitmap.Save(@"D:\БНТУ курс 2\РПВС\2 семестр\Dashboard\Dashboard\Output\salaryChart.png", ImageFormat.Png);

            Word wordCreator = new Word();

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

            wordCreator.LoadImage(@"D:\БНТУ курс 2\РПВС\2 семестр\Dashboard\Dashboard\Output\salaryChart.png", WdParagraphAlignment.wdAlignParagraphCenter);
            wordCreator.Visible = true;
            wordCreator.Dispose();

            buttonWord.Enabled = true;
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            buttonHelp.Enabled = false;
            Help.ShowHelp(this, @"D:\БНТУ курс 2\РПВС\2 семестр\Dashboard\Dashboard\Output\helpFile.chm");
            buttonHelp.Enabled = true;
        }

        
    }
}
