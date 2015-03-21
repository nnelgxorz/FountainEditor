using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public sealed class TextPointerFromTextService
    {
        private GetTextOffsetService getOffset;
        public TextPointerFromTextService(GetTextOffsetService getOffset)
        {
            this.getOffset = getOffset;
        }

        public TextPointer getPointer (FlowDocument Document, string text, bool contentEnd)
        {
            TextPointer pointer = Document.ContentStart;
            foreach (var block in Document.Blocks)
            {
                if (block is Paragraph)
                {
                    string currentText = new TextRange(block.ContentStart, block.ContentEnd).Text;

                    if (currentText == text)
                    {
                        if (contentEnd == true)
                        {
                            pointer = pointer.GetPositionAtOffset(getOffset.GetOffset(Document.ContentStart, block.ContentEnd));
                        }
                        else
                        {
                            pointer = pointer.GetPositionAtOffset(getOffset.GetOffset(Document.ContentStart, block.ContentStart));
                        }
                        break;
                    }
                }
            }
            return pointer;
        }
    }
}
