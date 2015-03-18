using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
    public sealed class TextTrimmerService : ITextTrimmer
    {
        public string TrimText(string text, int start, int end)
        {
            int End = start + end;
            text = text.Substring(start, text.Length - End);
            return text;
        }
    }
}
