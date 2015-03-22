using FountainEditorGUI.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using FountainEditor.Messaging;

namespace FountainEditorGUI
{
    public sealed class TextBoxDropAfterLogic
    {
        private int dropIndex;
        private GetParagraphIndexFromText getTextIndex;
        private GetPointerAtEndOfSectionHierarchy getEndOfHierarchy;
        private TextPointerFromTextService pointerFromText;
        private SelectHierachicalTextFromSectionParagraph selectHierarchy;
        private IMessagePublisher<OutlinerNavigationMessage> outlinerNavigationMessage;

        public TextBoxDropAfterLogic(GetParagraphIndexFromText getTextIndex, GetPointerAtEndOfSectionHierarchy getEndOfHierarchy,
            TextPointerFromTextService pointerFromText, SelectHierachicalTextFromSectionParagraph selectHierarchy,
            IMessagePublisher<OutlinerNavigationMessage> outlinerNavigationMessage)
        {
            this.getTextIndex = getTextIndex;
            this.getEndOfHierarchy = getEndOfHierarchy;
            this.pointerFromText = pointerFromText;
            this.selectHierarchy = selectHierarchy;
            this.outlinerNavigationMessage = outlinerNavigationMessage;
        }

        public FlowDocument Drop(RichTextBox DisplayBox, DragDropMessage message)
        {
            TextRange selection = selectHierarchy.Select(DisplayBox, message.dragItem, message.dragItemDepth);
            DisplayBox.Selection.Select(selection.Start, selection.End);
            DisplayBox.Cut();

            dropIndex = getTextIndex.getIndex(DisplayBox.Document, message.dropItem);
            TextPointer drop = getEndOfHierarchy.getPointer(DisplayBox.Document, message.dropItemDepth, dropIndex);

            if (drop.CompareTo(DisplayBox.Document.ContentEnd) == 0)
            {
                DisplayBox.Document.Blocks.Add(new Paragraph());
            }

            DisplayBox.Selection.Select(drop, drop);
            DisplayBox.Paste();
            outlinerNavigationMessage.Publish(new OutlinerNavigationMessage(message.dragItem));
            return DisplayBox.Document;
        }
    }
}
