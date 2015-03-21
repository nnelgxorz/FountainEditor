using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public sealed class GetTextElementIndex
    {
        private int index;
        public int getIndex (FlowDocument Document, string text)
        {
            for (int i = 0; i < Document.Blocks.Count; i++)
            {
                Block block = Document.Blocks.ElementAt(i);
                string currentText = new TextRange(block.ContentStart, block.ContentEnd).Text;

                if (currentText.Equals(text) && currentText.Length == text.Length)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
    }
}
