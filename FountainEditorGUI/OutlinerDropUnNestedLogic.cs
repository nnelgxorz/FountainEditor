using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using FountainEditorGUI.Messages;
using FountainEditor.Messaging;

namespace FountainEditorGUI
{
    public sealed class OutlinerDropUnNestedLogic
    {        
        private GenerateObservableCollectionFromHierarchy generateCollectionHierarchy;
        private RemoveElementsFromObservableCollection removeElementsFromCollection;
        private InsertObservableCollectionIntoObservableCollection insertCollectionInCollection;
        private GetEndOfOutlineSectionIndex getEndOfSectionIndex;
        private UnNestOutlinerCollectionItems unNestCollectionItems;
        private  IMessagePublisher<Messages.OutlinerSelectionMessage> outlinerSelectionMessage;
        public OutlinerDropUnNestedLogic(GenerateObservableCollectionFromHierarchy generateCollectionHierarchy, 
            RemoveElementsFromObservableCollection removeElementsFromCollection,
            InsertObservableCollectionIntoObservableCollection insertCollectionInCollection,
            GetEndOfOutlineSectionIndex getEndOfSectionIndex, UnNestOutlinerCollectionItems unNestCollectionItems,
            IMessagePublisher<OutlinerSelectionMessage> outlinerSelectionMessage)
        {
            this.generateCollectionHierarchy = generateCollectionHierarchy;
            this.removeElementsFromCollection = removeElementsFromCollection;
            this.insertCollectionInCollection = insertCollectionInCollection;
            this.getEndOfSectionIndex = getEndOfSectionIndex;
            this.unNestCollectionItems = unNestCollectionItems;
            this.outlinerSelectionMessage = outlinerSelectionMessage;
        }

        public ObservableCollection<string> Drop (ObservableCollection<string> collection, DragDropMessage message)
        {
            int insertIndex = message.dropIndex - 1;
            int amount = message.dragItemDepth - message.dropItemDepth;
            ObservableCollection<string> Selection = generateCollectionHierarchy.Generate(collection, message.dragIndex, message.dragItemDepth);

            Selection = unNestCollectionItems.UnNest(Selection, amount);
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
            return collection;
        }
    }
}
