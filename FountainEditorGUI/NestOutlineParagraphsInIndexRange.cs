using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Controls;

namespace FountainEditorGUI
{
    public sealed class NestOutlineParagraphsInIndexRange
    {
        private AddHashtags addHashTags;
        public NestOutlineParagraphsInIndexRange(AddHashtags addHashTags)
        {
            this.addHashTags = addHashTags;
        }

        public FlowDocument Nest(RichTextBox textBox, int start, int stop, int amount)
        {
            for (int i = start; i < stop; i++)
            {
                Paragraph paragraph = (Paragraph)textBox.Document.Blocks.ElementAt(i);
                string paragraphText = new TextRange(paragraph.ContentStart, paragraph.ContentEnd).Text;

                if (paragraphText.StartsWith("#"))
                {
                    string nestText = addHashTags.Add(paragraphText, amount);
                    paragraph.Inlines.Clear();
                    paragraph.Inlines.Add(new Run(nestText));
                }
            }
            return textBox.Document;
        }
    }
}
