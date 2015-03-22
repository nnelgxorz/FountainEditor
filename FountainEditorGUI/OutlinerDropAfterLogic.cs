using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using FountainEditorGUI.Messages;
using FountainEditor.Messaging;
using FountainEditorGUI;

namespace FountainEditorGUI
{
    public sealed class OutlinerDropAfterLogic
    {
        private GetEndOfHierarchicalElementsIndex getEndOfHierarchyIndex;
        private GenerateObservableCollectionFromHierarchy generateCollectionHierarchy;
        private RemoveElementsFromObservableCollection removeElementsFromCollection;
        private InsertObservableCollectionIntoObservableCollection insertCollectionInCollection;
        private IMessagePublisher<OutlinerSelectionMessage> outlinerSelectionMessage;
        public OutlinerDropAfterLogic(GetEndOfHierarchicalElementsIndex getEndOfHierarchyIndex, 
            GenerateObservableCollectionFromHierarchy generateCollectionHierarchy, 
            RemoveElementsFromObservableCollection removeElementsFromCollection,
            InsertObservableCollectionIntoObservableCollection insertCollectionInCollection,
            IMessagePublisher<OutlinerSelectionMessage> outlinerSelectionMessage)
        {
            this.getEndOfHierarchyIndex = getEndOfHierarchyIndex;
            this.generateCollectionHierarchy = generateCollectionHierarchy;
            this.removeElementsFromCollection = removeElementsFromCollection;
            this.insertCollectionInCollection = insertCollectionInCollection;
            this.outlinerSelectionMessage = outlinerSelectionMessage;
        }
        public ObservableCollection<string> Drop(ObservableCollection<string> collection, DragDropMessage message)
        {
            int insertIndex = getEndOfHierarchyIndex.GetIndex(collection, message.dragItemDepth, message.dropIndex);
            ObservableCollection<string> Selection = generateCollectionHierarchy.Generate(collection, message.dragIndex, message.dragItemDepth);

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
