using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FountainEditorGUI
{
    public sealed class MarkdownItalicFormat : IMarkdownFormatter
    {
        public string formatMarkdown(string text)
        {
            text = string.Format("*{0}*", text);
            return text;
        }
    }
}
