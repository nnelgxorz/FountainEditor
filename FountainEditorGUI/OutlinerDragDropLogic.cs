using FountainEditor.Messaging;
using FountainEditorGUI.Messages;
using System.Collections.ObjectModel;
using System.Linq;

namespace FountainEditorGUI
{
    public sealed class OutlinerDragDropLogic
    {
        private GenerateObservableCollectionFromHierarchy generateCollectionHierarchy;
        private RemoveElementsFromObservableCollection removeElementsFromCollection;
        private InsertObservableCollectionIntoObservableCollection insertCollectionInCollection;
        private GetEndOfHierarchicalElementsIndex getEndOfHierarchyIndex;
        private GetEndOfOutlineSectionIndex getEndOfSectionIndex;
        private CollectionItemsNestingService nestCollectionItems;
        private IMessagePublisher<OutlinerSelectionMessage> outlinerSelectionMessage;

        public OutlinerDragDropLogic(
            GenerateObservableCollectionFromHierarchy generateCollectionHierarchy,
            RemoveElementsFromObservableCollection removeElementsFromCollection,
            InsertObservableCollectionIntoObservableCollection insertCollectionInCollection,
            GetEndOfHierarchicalElementsIndex getEndOfHierarchyIndex,
            GetEndOfOutlineSectionIndex getEndOfSectionIndex,
            CollectionItemsNestingService nestCollectionItems,
            IMessagePublisher<OutlinerSelectionMessage> outlinerSelectionMessage)
        {
            this.generateCollectionHierarchy = generateCollectionHierarchy;
            this.removeElementsFromCollection = removeElementsFromCollection;
            this.insertCollectionInCollection = insertCollectionInCollection;
            this.getEndOfHierarchyIndex = getEndOfHierarchyIndex;
            this.getEndOfSectionIndex = getEndOfSectionIndex;
            this.nestCollectionItems = nestCollectionItems;
            this.outlinerSelectionMessage = outlinerSelectionMessage;
        }

        public ObservableCollection<string> DoDrop(ObservableCollection<string> collection, DragDropMessage message)
        {
            int insertIndex = message.dropIndex - 1;
            int amount = message.dropItemDepth - message.dragItemDepth;
            ObservableCollection<string> selection = generateCollectionHierarchy.Generate(collection, message.dragIndex, message.dragItemDepth);

            if (message.dropAction.Equals("After") || message.dropAction.Equals("Nest"))
            {
                insertIndex = getEndOfHierarchyIndex.GetIndex(collection, message.dragItemDepth, message.dropIndex);
            }

            selection = nestCollectionItems.DoNesting(selection, amount, message.dropAction);

            if (message.dragIndex > message.dropIndex)
            {
                removeElementsFromCollection.Remove(collection, message.dragIndex, selection.Count);
                insertCollectionInCollection.Insert(collection, selection, insertIndex);
            }
            else
            {
                insertCollectionInCollection.Insert(collection, selection, insertIndex);
                removeElementsFromCollection.Remove(collection, message.dragIndex, selection.Count);
            }

            outlinerSelectionMessage.Publish(new OutlinerSelectionMessage(selection.ElementAt(0)));
            selection.Clear();
            return collection;
        }
    }
}
