using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public class SectionParagraphsNestingService
    {
        private AddHashtags addHashTags;
        private RemoveHashtags removeHashTags;
        public SectionParagraphsNestingService(AddHashtags addHashTags, RemoveHashtags removeHashTags)
        {
            this.addHashTags = addHashTags;
            this.removeHashTags = removeHashTags;
        }
        public FlowDocument DoNesting(RichTextBox textBox, int start, int stop, int amount, string dropAction)
        {
            if (amount == 0 && !(dropAction.Equals("Nest")))
            {
                return textBox.Document;
            }

            for (int i = start; i < stop; i++)
            {
                Paragraph paragraph = (Paragraph)textBox.Document.Blocks.ElementAt(i);
                string paragraphText = new TextRange(paragraph.ContentStart, paragraph.ContentEnd).Text;
                string text = null;

                if (paragraphText.StartsWith("#"))
                {
                    if (amount < 0)
                    {
                        text = removeHashTags.Remove(paragraphText, amount * -1);
                    }
                    else
                    {
                        text = addHashTags.Add(paragraphText, amount + 1);
                    }
                    paragraph.Inlines.Clear();
                    paragraph.Inlines.Add(new Run(text));
                }
            }
            return textBox.Document;
        }
    }
}
