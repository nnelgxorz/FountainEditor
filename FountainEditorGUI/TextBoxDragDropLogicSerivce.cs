using FountainEditor.Messaging;
using FountainEditorGUI.Messages;
using System.Windows.Controls;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public sealed class TextBoxDragDropLogicSerivce
    {
        private SelectHierachicalTextFromSectionParagraph selectHierarchicalTextSection;
        private GetParagraphIndexFromTextPointer indexFromPointer;
        private SectionParagraphsNestingService NestParagraphs;
        private ITextScanner textScanner;
        private IMessagePublisher<OutlinerNavigationMessage> outlinerNavigationMessage;

        public TextBoxDragDropLogicSerivce(
            SelectHierachicalTextFromSectionParagraph selectHierarchicalTextSection,
            GetParagraphIndexFromTextPointer getIndexFromPointer,
            SectionParagraphsNestingService NestParagraphs,
            ITextScanner textScanner,
            IMessagePublisher<OutlinerNavigationMessage> outlinerNavigationMessage)
        {
            this.selectHierarchicalTextSection = selectHierarchicalTextSection;
            this.indexFromPointer = getIndexFromPointer;
            this.NestParagraphs = NestParagraphs;
            this.textScanner = textScanner;
            this.outlinerNavigationMessage = outlinerNavigationMessage;
        }

        public FlowDocument DoDrop(RichTextBox textBox, DragDropMessage message)
        {
            int amount = message.dropItemDepth - message.dragItemDepth;

            TextRange selection = selectHierarchicalTextSection.Select(textBox, message.dragItem, message.dragItemDepth);
            textBox.Selection.Select(selection.Start, selection.End);
            textBox.Cut();

            TextRange dropSelection = selectHierarchicalTextSection.Select(textBox, message.dropItem, message.dragItemDepth);

            textBox.Document.Blocks.Add(new Paragraph());

            if (message.dropAction.Equals("Before"))
            {
                textBox.Selection.Select(dropSelection.Start, dropSelection.Start);
            }
            else
            {
                textBox.Selection.Select(dropSelection.End, dropSelection.End);
            }

            textBox.Paste();
            textBox.Document.Blocks.Remove(textBox.Document.Blocks.LastBlock);

            selection = selectHierarchicalTextSection.Select(textBox, message.dragItem, message.dragItemDepth);
            int start = indexFromPointer.getIndex(textBox, selection.Start);
            int end = indexFromPointer.getIndex(textBox, selection.End);

            //textBox.Document = NestParagraphs.DoNesting(textBox, start, end, amount, message.dropAction);

            outlinerNavigationMessage.Publish(new OutlinerNavigationMessage(textScanner.ScanForText(selection.Start)));
            return textBox.Document;
        }
    }
}
