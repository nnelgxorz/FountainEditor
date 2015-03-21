using System.Collections.Generic;
using System.Linq;
using FountainEditor.Messaging;
using FountainEditor.ObjectModel;
using FountainEditorGUI.Messages;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace FountainEditorGUI.ViewModels
{
    public sealed class FountainOutlineViewModel : ViewModelBase
    {
        private int dragIndex;
        private int dropIndex;
        private string dragItem;
        private string dropItem;
        public ObservableCollection<string> documentOutline;
        private GenerateObservableCollectionFromHierarchy generateCollectionHierarchy;
        private InsertObservableCollectionIntoObservableCollection insertCollectionInCollection;
        private RemoveElementsFromObservableCollection removeElementsFromCollection;
        private GetEndOfHierachicalElementsIndex getEndOfHierarchyIndex;

        public ObservableCollection<string> DocumentOutline {
            get {
                return documentOutline;
            }
        }

        public FountainOutlineViewModel(IMessagePublisher<DocumentMessage> documentMessagePublisher, 
            IMessagePublisher<DragDropMessage> dragDropMessagePublisher, GenerateObservableCollectionFromHierarchy generateCollectionHierarchy,
            InsertObservableCollectionIntoObservableCollection insertCollectionInCollection, RemoveElementsFromObservableCollection removeElementsFromCollection,
            GetEndOfHierachicalElementsIndex GetEndOfHierarchyIndex)
        {
            documentMessagePublisher.Subscribe(DocumentChanged);
            dragDropMessagePublisher.Subscribe(DragAndDrop);
            this.generateCollectionHierarchy = generateCollectionHierarchy;
            this.insertCollectionInCollection = insertCollectionInCollection;
            this.removeElementsFromCollection = removeElementsFromCollection;
            this.getEndOfHierarchyIndex = GetEndOfHierarchyIndex;
        }

        private void DocumentChanged(DocumentMessage message)
        {
            this.documentOutline = new ObservableCollection<string>(from element in message.Document
                                                                    where element is SectionElement || element is SynopsisElement
                                                                    select element.Text);
            OnPropertyChanged("DocumentOutline");
        }

        private void DragAndDrop(DragDropMessage message)
        {
            this.dragIndex = message.dragIndex;
            this.dropIndex = message.dropIndex;
            this.dragItem = message.dragItem;
            this.dropItem = message.dropItem;

            if (dragIndex < 0 | dropIndex < 0)
            {
                return;
            }
            int insertIndex = getEndOfHierarchyIndex.GetIndex(documentOutline, message.dragItemDepth, dropIndex);
            ObservableCollection<string> Selection = generateCollectionHierarchy.Generate(documentOutline, dragIndex, message.dragItemDepth);

            if (dragIndex > dropIndex)
            {
                removeElementsFromCollection.Remove(documentOutline, dragIndex, Selection.Count);
                insertCollectionInCollection.Insert(documentOutline, Selection, insertIndex);
            }
            else
            {
                insertCollectionInCollection.Insert(documentOutline, Selection, insertIndex);
                removeElementsFromCollection.Remove(documentOutline, dragIndex, Selection.Count);
            }
            
            Selection.Clear();
        }
    }
}
