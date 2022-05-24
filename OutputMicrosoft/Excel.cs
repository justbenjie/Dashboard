using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;

namespace OutputMicrosoft
{
    public class Excel
    {
        static public void Show(string file)
        {
            Application app = new Application();
            Workbook workbook = app.Workbooks.Open(file);
            app.Visible = true;
        }
    }
}
