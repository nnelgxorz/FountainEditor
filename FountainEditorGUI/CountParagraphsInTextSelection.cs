using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public sealed class CountParagraphsInTextSelection
    {
        private GetParagraphIndexFromTextPointer indexFromPointer;
        public CountParagraphsInTextSelection(GetParagraphIndexFromTextPointer indexFromPointer)
        {
            this.indexFromPointer = indexFromPointer;
        }
        public int Count(RichTextBox textBox, TextRange textRange)
        {
            int count = 0;

            int startIndex = indexFromPointer.getIndex(textBox, textRange.Start);
            int stopIndex = indexFromPointer.getIndex(textBox, textRange.End);

            for (int i = startIndex; i < stopIndex; i++)
            {
                count++;
            }

            return count;
        }
    }
}
