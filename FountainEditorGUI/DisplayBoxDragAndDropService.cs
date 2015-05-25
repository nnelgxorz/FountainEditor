using FountainEditor.Messaging;
using FountainEditorGUI.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public sealed class DisplayBoxDragAndDropService
    {
        private FindSectionElementIndexFromText findSectionElementIndex;
        private FindEndOfSectionHierarchyIndex findEndOfHierarchyText;
        private SectionParagraphsNestingService nestingService;
        private ReOrderSectionIndicesService reOrderSectionIndices;
        private AddRemoveItemsToList addRemoveItems;
        private GenerateSubListOfSectionIndices generateSubList;
        private ITextCounter textCounter;
        private IMessagePublisher<OutlinerNavigationMessage> outlineNavigationMessage;
        public DisplayBoxDragAndDropService(
            FindSectionElementIndexFromText findSectionElementIndex,
            FindEndOfSectionHierarchyIndex findEndOfHierarchyText,
            SectionParagraphsNestingService nestingService,
            ReOrderSectionIndicesService reOrderSectionIndices,
            AddRemoveItemsToList addRemoveItems,
            GenerateSubListOfSectionIndices generateSubList,
            ITextCounter textCounter,
            IMessagePublisher<OutlinerNavigationMessage> outlineNavigationMessage)
        {
            this.findSectionElementIndex = findSectionElementIndex;
            this.findEndOfHierarchyText = findEndOfHierarchyText;
            this.reOrderSectionIndices = reOrderSectionIndices;
            this.nestingService = nestingService;
            this.addRemoveItems = addRemoveItems;
            this.generateSubList = generateSubList;
            this.textCounter = textCounter;
            this.outlineNavigationMessage = outlineNavigationMessage;
        }

        public FlowDocument Drop(RichTextBox textBox, List<SectionIndexClass> indices, DragDropMessage message)
        {
            var blocks = textBox.Document.Blocks;
            int amount = message.dropItemDepth - message.dragItemDepth;
            int startDrag = findSectionElementIndex.Find(indices, message.dragItem);
            int endDrag = findEndOfHierarchyText.Find(indices, startDrag);
            List<SectionIndexClass> sublist = generateSubList.Generate(indices, startDrag, endDrag);

            TextPointer cutStart = blocks.ElementAt(indices.ElementAt(startDrag).index).ContentStart;
            TextPointer cutEnd = cutStart;
            if (startDrag == indices.Count - 1)
            {
                cutEnd = textBox.Document.ContentEnd;
            }
            else
            {
                cutEnd = blocks.ElementAt(indices.ElementAt(endDrag).index).ContentStart;
            }
            textBox.Selection.Select(cutStart, cutEnd);
            textBox.Cut();

            indices = addRemoveItems.Remove(indices, startDrag, endDrag);
            indices = reOrderSectionIndices.ReOrder(indices);

            int startDrop = findSectionElementIndex.Find(indices, message.dropItem);
            int endDrop = findEndOfHierarchyText.Find(indices, startDrop);

            int insert = 0;
            TextPointer InsertPointer;
            if (message.dropAction.Equals("Before"))
            {
                insert = startDrop;
                InsertPointer = blocks.ElementAt(indices.ElementAt(startDrop).index).ContentStart;
            }
            else
            {
                insert = endDrop;
                if (startDrop == indices.Count - 1)
                {
                    InsertPointer = textBox.Document.ContentEnd;
                    textBox.CaretPosition = InsertPointer;
                    textBox.CaretPosition.InsertParagraphBreak();
                }
                else
                {
                    InsertPointer = blocks.ElementAt(indices.ElementAt(endDrop).index).ContentStart;
                }
            }
            textBox.Selection.Select(InsertPointer, InsertPointer);
            textBox.Paste();

            indices = addRemoveItems.Add(indices, sublist, insert, message);
            indices = reOrderSectionIndices.ReOrder(indices);

            int start = findSectionElementIndex.Find(indices, message.dragItem);
            int end = findEndOfHierarchyText.Find(indices, start);
            int startFormat = indices.ElementAt(start).index;
            int endFormat = indices.ElementAt(end).index;

            textBox.Document = nestingService.DoNesting(textBox, indices, start, end, amount, message.dropAction);
            outlineNavigationMessage.Publish(new OutlinerNavigationMessage(indices.ElementAt(start).text));

            return textBox.Document;
        }
    }
}
