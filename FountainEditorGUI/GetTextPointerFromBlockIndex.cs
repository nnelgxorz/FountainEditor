using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public sealed class GetTextPointerFromBlockIndex
    {
        public TextPointer getPointer(FlowDocument document, int index)
        {
            TextPointer pointer = document.ContentStart;

            if (index > document.Blocks.Count)
            {
                index = document.Blocks.Count;
                pointer = document.Blocks.ElementAt(index).ContentEnd;
            }
            else
            {
                pointer = document.Blocks.ElementAt(index).ContentStart;
            }
            
            return pointer;
        }
    }
}
