using FountainEditorGUI.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FountainEditor.Messaging;

namespace FountainEditorGUI
{
    public sealed class OutlinerDropNestedLogic
    {
        private GenerateObservableCollectionFromHierarchy generateCollectionHierarchy;
        private RemoveElementsFromObservableCollection removeElementsFromCollection;
        private InsertObservableCollectionIntoObservableCollection insertCollectionInCollection;
        private GetEndOfOutlineSectionIndex getEndOfSectionIndex;
        private NestOutlinerCollectionItems NestCollectionItems;
        private  IMessagePublisher<Messages.OutlinerSelectionMessage> outlinerSelectionMessage;
        public OutlinerDropNestedLogic(GenerateObservableCollectionFromHierarchy generateCollectionHierarchy, 
            RemoveElementsFromObservableCollection removeElementsFromCollection,
            InsertObservableCollectionIntoObservableCollection insertCollectionInCollection,
            GetEndOfOutlineSectionIndex getEndOfSectionIndex, NestOutlinerCollectionItems NestCollectionItems,
            IMessagePublisher<OutlinerSelectionMessage> outlinerSelectionMessage)
        {
            this.generateCollectionHierarchy = generateCollectionHierarchy;
            this.removeElementsFromCollection = removeElementsFromCollection;
            this.insertCollectionInCollection = insertCollectionInCollection;
            this.getEndOfSectionIndex = getEndOfSectionIndex;
            this.NestCollectionItems = NestCollectionItems;
            this.outlinerSelectionMessage = outlinerSelectionMessage;
        }
        public ObservableCollection<string> Drop(ObservableCollection<string> collection, DragDropMessage message)
        {
            int insertIndex = getEndOfSectionIndex.GetIndex(collection, message.dropIndex);
            int amount = message.dropItemDepth - message.dragItemDepth + 1;
            ObservableCollection<string> Selection = generateCollectionHierarchy.Generate(collection, message.dragIndex, message.dragItemDepth);

            Selection = NestCollectionItems.Nest(Selection, amount);
            if (message.dragIndex > message.dropIndex)
            {
                removeElementsFromCollection.Remove(collection, message.dragIndex, Selection.Count);
                insertCollectionInCollection.Insert(collection, Selection, insertIndex);
            }
            else
            {
                insertCollectionInCollection.Insert(collection, Selection, insertIndex);
                removeElementsFromCollection.Remove(collection, message.dragIndex, Selection.Count);
            }
            outlinerSelectionMessage.Publish(new OutlinerSelectionMessage(Selection.ElementAt(0)));
            Selection.Clear();
            return collection;
        }
    }
}
