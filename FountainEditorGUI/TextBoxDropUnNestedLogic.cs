using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FountainEditorGUI.Messages;
using FountainEditor.Messaging;
using System.Windows.Controls;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public sealed class TextBoxDropUnNestedLogic
    {
        private SelectHierachicalTextFromSectionParagraph selectHierarchicalText;
        private TextPointerFromTextService pointerFromText;
        private GetParagraphIndexFromTextPointer indexFromPointer;
        private UnNestOutlineParagraphsInIndexRange unNestParagraphs;
        private IMessagePublisher<OutlinerNavigationMessage> outlinerNavigationMessage;

        public TextBoxDropUnNestedLogic(SelectHierachicalTextFromSectionParagraph selectHierarchicalText, 
            TextPointerFromTextService pointerFromText, IMessagePublisher<OutlinerNavigationMessage> outlinerNavigationMessage, 
            GetParagraphIndexFromTextPointer indexFromPointer, UnNestOutlineParagraphsInIndexRange unNestParagraphs)
        {
            this.selectHierarchicalText = selectHierarchicalText;
            this.pointerFromText = pointerFromText;
            this.outlinerNavigationMessage = outlinerNavigationMessage;
            this.indexFromPointer = indexFromPointer;
            this.unNestParagraphs = unNestParagraphs;
        }

        public FlowDocument Drop (RichTextBox textBox, DragDropMessage message)
        {
            TextRange Selection = selectHierarchicalText.Select(textBox, message.dragItem, message.dragItemDepth);
            textBox.Selection.Select(Selection.Start, Selection.End);
            textBox.Cut();

            TextPointer drop = pointerFromText.getPointer(textBox.Document, message.dropItem, false);
            textBox.Selection.Select(drop, drop);
            textBox.Paste();

            Selection = selectHierarchicalText.Select(textBox, message.dragItem, message.dragItemDepth);
            int start = indexFromPointer.getIndex(textBox, Selection.Start);
            int end = indexFromPointer.getIndex(textBox, Selection.End);
            int amount = message.dragItemDepth - message.dropItemDepth;
            textBox.Document = unNestParagraphs.UnNest(textBox, start, end, amount);

            return textBox.Document;
        }
    }
}
