using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;


namespace FountainEditorGUI
{
    public sealed class UnNestOutlineParagraphsInIndexRange
    {
        private RemoveHashtags removeHashtags;
        public UnNestOutlineParagraphsInIndexRange(RemoveHashtags removeHashtags)
        {
            this.removeHashtags = removeHashtags;
        }
        public FlowDocument UnNest (RichTextBox textBox, int start, int end, int amount)
        {
            for (int i = start; i < end; i++)
            {
                var paragraph = (Paragraph)textBox.Document.Blocks.ElementAt(i);
                var paragraphText = new TextRange(paragraph.ContentStart, paragraph.ContentEnd).Text;

                if (paragraphText.StartsWith("#"))
                {
                    string nestText = removeHashtags.Remove(paragraphText, amount);
                    paragraph.Inlines.Clear();
                    paragraph.Inlines.Add(new Run(nestText));
                }
            }

            return textBox.Document;
        }  
    }
}

