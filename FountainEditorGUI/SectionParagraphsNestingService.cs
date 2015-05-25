using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Collections.Generic;
using FountainEditorGUI.Messages;
using FountainEditor.Messaging;

namespace FountainEditorGUI
{
    public class SectionParagraphsNestingService
    {
        private AddHashtags addHashTags;
        private RemoveHashtags removeHashTags;
        private IMessagePublisher<SectionIndicesChangedMessage> sectionIndicesMessage;
        public SectionParagraphsNestingService(
            AddHashtags addHashTags, 
            RemoveHashtags removeHashTags,
            IMessagePublisher<SectionIndicesChangedMessage> sectionIndicesMessage)
        {
            this.addHashTags = addHashTags;
            this.removeHashTags = removeHashTags;
            this.sectionIndicesMessage = sectionIndicesMessage;
        }
        public FlowDocument DoNesting(RichTextBox textBox, List<SectionIndexClass> indices,
            int start, int stop, int difference, string dropAction)
        {
            if (difference == 0 && !(dropAction.Equals("Nest")))
            {
                return textBox.Document;
            }

            string text = null;
            if (start == stop)
            {
                var current = indices.ElementAt(start);
                var index = current.index;
                Paragraph paragraph = (Paragraph)textBox.Document.Blocks.ElementAt(index);
                string paragraphText = new TextRange(paragraph.ContentStart, paragraph.ContentEnd).Text;

                if (difference < 0)
                {
                    int amount = -difference;
                    text = removeHashTags.Remove(paragraphText, amount);
                    current.hashCount = current.hashCount + difference;
                }
                else
                {
                    int amount = difference + 1;
                    text = addHashTags.Add(paragraphText, amount);
                    current.hashCount = current.hashCount + amount;
                }
                paragraph.Inlines.Clear();
                paragraph.Inlines.Add(new Run(text));
                current.text = text;
            }
            else
            {

                for (int i = start; i < stop; i++)
                {
                    var current = indices.ElementAt(i);
                    var index = current.index;
                    Paragraph paragraph = (Paragraph)textBox.Document.Blocks.ElementAt(index);
                    string paragraphText = new TextRange(paragraph.ContentStart, paragraph.ContentEnd).Text;

                    if (difference < 0)
                    {
                        int amount = -difference;
                        text = removeHashTags.Remove(paragraphText, amount);
                        current.hashCount = current.hashCount + difference;
                    }
                    else
                    {
                        int amount = difference + 1;
                        text = addHashTags.Add(paragraphText, amount);
                        current.hashCount = current.hashCount + amount;
                    }
                    paragraph.Inlines.Clear();
                    paragraph.Inlines.Add(new Run(text));
                    current.text = text;
                }
            }

            sectionIndicesMessage.Publish(new SectionIndicesChangedMessage(indices));
            return textBox.Document;
        }
    }
}
