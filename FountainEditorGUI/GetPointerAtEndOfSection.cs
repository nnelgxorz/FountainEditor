using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public sealed class GetPointerAtEndOfSection
    {
        public TextPointer getPointer(FlowDocument Document, int index)
        {
            TextPointer endSelection = Document.ContentEnd;

            for (int i = index + 1; i < Document.Blocks.Count; i++)
            {
                Block block = Document.Blocks.ElementAt(i);
                string currentText = new TextRange(block.ContentStart, block.ContentEnd).Text;

                if (currentText.StartsWith("#"))
                {
                    endSelection = block.ContentStart;
                    break;
                }
            }

            return endSelection;
        }
    }
}
