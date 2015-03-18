using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorGUI
{
     public sealed class MarkdownBoldFormat : IMarkdownFormatter
    {
        public string formatMarkdown(string text)
        {
            text = (string.Format("**{0}**", text));
            return text;
        }
    }
}
