using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using FountainEditorGUI.Messages;
using FountainEditor.Messaging;
namespace FountainEditorGUI
{
    public sealed class TextBoxDropBeforeLogic
    {
        private SelectHierachicalTextFromSectionParagraph selectHierarchicalTextSection;
        private TextPointerFromTextService pointerFromText;
        private IMessagePublisher<OutlinerNavigationMessage> outlinerNavigationMessagePublisher;
        public TextBoxDropBeforeLogic(SelectHierachicalTextFromSectionParagraph selectHierarchicalTextSection,
            TextPointerFromTextService pointerFromText, IMessagePublisher<OutlinerNavigationMessage> outlinerNavigationMessagePublisher)
        {
            this.selectHierarchicalTextSection = selectHierarchicalTextSection;
            this.pointerFromText = pointerFromText;
            this.outlinerNavigationMessagePublisher = outlinerNavigationMessagePublisher;
        }

        public FlowDocument Drop (RichTextBox textBox, DragDropMessage message)
        {
            TextRange selection = selectHierarchicalTextSection.Select(textBox, message.dragItem, message.dragItemDepth);
            textBox.Selection.Select(selection.Start, selection.End);
            textBox.Cut();

            TextPointer drop = pointerFromText.getPointer(textBox.Document, message.dropItem, false);
            textBox.Selection.Select(drop, drop);
            textBox.Paste();

            outlinerNavigationMessagePublisher.Publish(new OutlinerNavigationMessage(message.dragItem));
            return textBox.Document;
        }
    }
}
