using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;


namespace OutputMicrosoft
{
    public class Excel
    {
        private Application _excelApp;
        private Workbook _workbook;
        private Worksheet _worksheet;
        

        public Excel()
        {
            CreateExcel();
            
        }

        public bool Visible
        {
            set { _excelApp.Visible = value; }
            get { return _excelApp.Visible; }
        }

        public bool Interactive
        {
            set { _excelApp.Interactive = value; }
            get { return _excelApp.Interactive; }
        }

        private void CreateExcel()
        {
            _excelApp = new Application();
            _excelApp.Visible = false;
            _excelApp.Interactive = false;
            _workbook = (Workbook)_excelApp.Workbooks.Add( System.Type.Missing);
            _worksheet = (Worksheet)_workbook.ActiveSheet;

        }

        public void AddDict(Dictionary<string, int> dict)
        {

            int column = 1; // Initialize for keys.
            foreach (string key in dict.Keys)
            {
                int row = 1; // Initialize for values in key.
                _worksheet.Cells[row, column] = key;

               
                row++;
                _worksheet.Cells[row, column] = dict[key];
                

                column++; // increment for next key.
            }
        }

        public void AddChart()
        {
            Range chartRange;
            ChartObjects xlCharts = (ChartObjects)_worksheet.ChartObjects();
            ChartObject chartObj = (ChartObject)xlCharts.Add(468, 160, 348, 268);
            Chart chart = chartObj.Chart;

            chartRange = _worksheet.Range[_worksheet.Cells[1, 1], _worksheet.Cells[2, 3]];
            chart.SetSourceData(chartRange, System.Type.Missing);
            chart.ChartType = XlChartType.xlPieExploded;
            chart.HasTitle = true;
            chart.ChartTitle.Text = "Schedule";
            chart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowPercent, XlDataLabelsType.xlDataLabelsShowLabel, true, false, false, true, false, true);
        }
        public void Dispose()
        {
            // Close document
            if (_workbook != null)
                Marshal.ReleaseComObject(_workbook);
            if (_workbook != null)
                Marshal.ReleaseComObject(_workbook);
        }
    }
}
