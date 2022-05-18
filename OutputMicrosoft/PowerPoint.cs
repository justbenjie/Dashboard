using System.Runtime.InteropServices;
using Microsoft.Office.Interop.PowerPoint;

namespace OutputMicrosoft
{
    public class PowerPoint
    {
        static public void Show(string file)
        {
            Application app = new Application();
            Presentation presentation = app.Presentations.Open2007(file);
            SlideShowSettings objSSS;
            objSSS = presentation.SlideShowSettings;
            objSSS.Run();
            Marshal.FinalReleaseComObject(app);
        }
    }
}
