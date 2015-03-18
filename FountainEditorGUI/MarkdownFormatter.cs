using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public sealed class MarkdownFormatter
    {
        private MarkdownBoldFormat Bold;
        private MarkdownItalicFormat Italic;
        private MarkdownUnderlineFormat Underline;
        public MarkdownFormatter(MarkdownBoldFormat Bold, MarkdownItalicFormat Italic, MarkdownUnderlineFormat Underline)
        {
            this.Bold = Bold;
            this.Italic = Italic;
            this.Underline = Underline;
        }
        public string format (TextRange textRange)
        {
            var bold = textRange.GetPropertyValue(TextElement.FontWeightProperty);
            var italic = textRange.GetPropertyValue(TextElement.FontStyleProperty);
            var underline = textRange.GetPropertyValue(Inline.TextDecorationsProperty);
            string text = textRange.Text;

            if (bold.Equals(FontWeights.Bold))
            {
                text = Bold.formatMarkdown(text);
            }

            if (italic.Equals(FontStyles.Italic))
            {
                text = Italic.formatMarkdown(text);
            }

            if (underline.Equals(TextDecorations.Underline))
            {
                text = Underline.formatMarkdown(text);
            }

            return text;
        }
    }
}
