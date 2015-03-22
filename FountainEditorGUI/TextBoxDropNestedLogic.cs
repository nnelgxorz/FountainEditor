using FountainEditorGUI.Messages;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using FountainEditor.Messaging;

namespace FountainEditorGUI
{
    public sealed class TextBoxDropNestedLogic
    {
        private int dropIndex;
        private GetParagraphIndexFromText getTextIndex;
        private AddHashtags addHashtags;
        private NestOutlineParagraphsInIndexRange nestParagraphs;
        private SelectHierachicalTextFromSectionParagraph selectHierarchy;
        private CountParagraphsInTextSelection countParagraphsInSelection;
        private GetPointerAtEndOfSection getEndOfSection;
        private IMessagePublisher<OutlinerNavigationMessage> outlineNavigationMessage;

        public TextBoxDropNestedLogic(GetParagraphIndexFromText getTextIndex, AddHashtags addHashtags, 
            IMessagePublisher<OutlinerNavigationMessage> outlineNavigationMessage, 
            NestOutlineParagraphsInIndexRange nestParagraphs, SelectHierachicalTextFromSectionParagraph selectHierarchy, 
            GetParagraphIndexFromTextPointer getIndexFromPointer, CountParagraphsInTextSelection countParagraphsInSelection,
            GetPointerAtEndOfSection getEndOfSection)
        {
            this.getTextIndex = getTextIndex;
            this.addHashtags = addHashtags;
            this.outlineNavigationMessage = outlineNavigationMessage;
            this.nestParagraphs = nestParagraphs;
            this.selectHierarchy = selectHierarchy;
            this.countParagraphsInSelection = countParagraphsInSelection;
            this.getEndOfSection = getEndOfSection;
        }

        public FlowDocument Drop(RichTextBox DisplayBox, DragDropMessage message)
        {
            int nestAmount = message.dropItemDepth - message.dragItemDepth + 1;
            TextRange Selection = selectHierarchy.Select(DisplayBox, message.dragItem, message.dragItemDepth);
            dropIndex = getTextIndex.getIndex(DisplayBox.Document, message.dropItem);
            TextPointer drop = getEndOfSection.getPointer(DisplayBox.Document, dropIndex);
            
            if (drop.CompareTo(DisplayBox.Document.ContentEnd) == 0)
            {
                DisplayBox.Document.Blocks.Add(new Paragraph());
            }

            int count = countParagraphsInSelection.Count(DisplayBox, Selection);

            DisplayBox.Selection.Select(Selection.Start, Selection.End);
            DisplayBox.Cut();


            DisplayBox.Selection.Select(drop, drop);
            DisplayBox.Paste();

            Selection = selectHierarchy.Select(DisplayBox, message.dragItem, message.dragItemDepth);
            int startNesting = getTextIndex.getIndex(DisplayBox.Document, message.dragItem);
            int endNesting = startNesting + count - 1;
            DisplayBox.Document = nestParagraphs.Nest(DisplayBox, startNesting, endNesting, nestAmount);

            outlineNavigationMessage.Publish(new OutlinerNavigationMessage(addHashtags.Add(message.dragItem, nestAmount)));
            return DisplayBox.Document;
        }
    }
}
