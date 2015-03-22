using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public sealed class GetParagraphIndexFromTextPointer
    {
        private GetParagraphIndexFromText getIndexFromText;
        public GetParagraphIndexFromTextPointer(GetParagraphIndexFromText getIndexFromText)
        {
            this.getIndexFromText = getIndexFromText;
        }
        public int getIndex(RichTextBox textBox, TextPointer pointer)
        {
            TextPointer start = pointer.Paragraph.ContentStart;
            TextPointer end = pointer.Paragraph.ContentEnd;

            string text = new TextRange(start, end).Text;
            int index = getIndexFromText.getIndex(textBox.Document, text);

            return index;
        }
    }
}
