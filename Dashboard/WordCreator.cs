using Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DataPresentation
{
    public class WordCreator
    {
        private Microsoft.Office.Interop.Word.Application _wordApp;
        private Document _wordDoc;
        private Paragraph _paragraph;
        private string _style = "Обычный";
        private string _fontName = "Times New Roman";

        public WordCreator()
        {
            CreateWord();
            _wordApp.Visible = false;
        }

        private void CreateWord()
        {
            _wordApp = new Microsoft.Office.Interop.Word.Application();
            _wordDoc = _wordApp.Documents.Add();
            _wordApp.DisplayAlerts = WdAlertLevel.wdAlertsNone;
        }

        public bool Visible
        {
            set { _wordApp.Visible = value; }
            get { return _wordApp.Visible; }
        }

        public string Style
        {
            set { _style = value; }
            get { return _style; }
        }

        public string FontName
        {
            set { _fontName = value; }
            get { return _fontName; }
        }

        public void AddParagraph(string text, WdParagraphAlignment wdParagraphAlignment)
        {
            _paragraph = _wordDoc.Paragraphs.Add();
            _paragraph.Range.Text = text;
            _paragraph.Range.set_Style(_style);
            _paragraph.Range.Font.Name = _fontName;
            _paragraph.Alignment = wdParagraphAlignment;
            _paragraph.Range.InsertParagraphAfter();
        }

        public void AddText(string text, WdParagraphAlignment wdParagraphAlignment)
        {
            AddParagraph(text, wdParagraphAlignment);
        }

        public void Enter()
        {
           _paragraph = _wordDoc.Paragraphs.Add();
        }

        public void LoadImage(string nameImage, WdParagraphAlignment wdParagraphAlignment)
        {
            _paragraph = _wordDoc.Paragraphs.Add();
            _paragraph.Range.InlineShapes.AddPicture(nameImage, Range: _paragraph.Range);
            _paragraph.Alignment = wdParagraphAlignment;
        }

        public void CreateGrid(int n, int m, List<string> columnNames, List<string> data)
        {
            Table table = _wordDoc.Tables.Add(_paragraph.Range, n, m, WdDefaultTableBehavior.wdWord9TableBehavior, WdAutoFitBehavior.wdAutoFitContent);

            for (int i = 1; i <= columnNames.Count; i++)
                table.Cell(1, i).Range.Text = columnNames[i - 1];

            for (int j = 2, count = 0; j <= n; j++)
                for (int i = 1; i <= m; i++, count++)
                    table.Cell(i, j).Range.Text = data[count];
        }

        public void Dispose()
        {
            // Закрыть.
            if (_wordDoc != null)
                Marshal.ReleaseComObject(_wordDoc);
            if (_wordApp != null)
                Marshal.ReleaseComObject(_wordApp);
        }
    }
}
