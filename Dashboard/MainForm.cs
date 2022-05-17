using ApiClient;
using ColumnCharts;
using PieCharts;
using RowCharts;
using System;
using System.Threading;
using System.Windows.Forms;
using DataPresentation;
using Microsoft.Office.Interop.Word;
using System.Drawing.Imaging;
using System.Windows.Interop;
using System.Drawing;

namespace Dashboard
{
    public partial class MainForm : Form
    {
        private Piechart pieChart;
        private Piechart pieChart2;
        private Columnchart columnChart;
        private Rowchart rowChart;
        private VacanciesInfo vacanciesInfo;

        public MainForm()
        {
            Thread thread = new Thread(new ThreadStart(StartForm));
            thread.Start();
            InitializeComponent();
            UpdatedForm();
            Thread.Sleep(5000);
            thread.Abort();
        }

        private void StartForm()
        {
            System.Windows.Forms.Application.Run(new SplashScreen());
        }

        private async void UpdatedForm()
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

            pieChart2 = new Piechart(schedule);
            pieChart2.DrawPiechart(vacanciesInfo.Schedule);

            rowChart = new Rowchart(skills);
            rowChart.DrawRowchart(vacanciesInfo.Skills);

            columnChart = new Columnchart(salary);
            columnChart.DrawColumnchart(vacanciesInfo.Salary.SalaryExpMedian);
            

        }

        private void update_Click(object sender, EventArgs e)
        {
            UpdatedForm();
        }

        private void PowerPoint_Click(object sender, EventArgs e)
        {
            PowerPoint.Enabled = false;
            PowerPointCreator.Show(@"D:\БНТУ курс 2\РПВС\2 семестр\Dashboard\Dashboard\Output\Анализ вакансий с HeadHunter.pptx");
            PowerPoint.Enabled = true;
        }

        private void word_Click(object sender, EventArgs e)
        {
            word.Enabled = false;

            Bitmap bitmap = new Bitmap(salary.Size.Width, salary.Size.Height);    
            salary.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height));
            bitmap.Save(@"D:\БНТУ курс 2\РПВС\2 семестр\Dashboard\Dashboard\Output\salaryChart.png", ImageFormat.Png);

            WordCreator wordCreator = new WordCreator();

            wordCreator.AddParagraph(textBox1.Text, WdParagraphAlignment.wdAlignParagraphCenter);
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

            word.Enabled = true;
        }

        private void help_Click(object sender, EventArgs e)
        {
            help.Enabled = false;
            Help.ShowHelp(this, @"D:\БНТУ курс 2\РПВС\2 семестр\Dashboard\Dashboard\Output\helpFile.chm");
            help.Enabled = true;
        }

        
    }
}
