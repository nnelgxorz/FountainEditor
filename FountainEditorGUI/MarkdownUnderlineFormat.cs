using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FountainEditorGUI
{
    public sealed class MarkdownUnderlineFormat : IMarkdownFormatter
    {
        public string formatMarkdown(string text)
        {
            text = string.Format("_{0}_", text);
            return text;
        }
    }
}
