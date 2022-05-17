using Microsoft.Office.Interop.PowerPoint;
using System.Runtime.InteropServices;

namespace DataPresentation
{
    public class PowerPointCreator
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
