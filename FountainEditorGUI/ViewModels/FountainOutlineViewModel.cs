using System.Collections.Generic;
using System.Linq;
using FountainEditor.Messaging;
using FountainEditor.ObjectModel;
using FountainEditorGUI.Messages;
using System.Collections.ObjectModel;

namespace FountainEditorGUI.ViewModels
{
    public sealed class FountainOutlineViewModel : ViewModelBase
    {
        private int dragIndex;
        private int dropIndex;
        private string dragItem;
        public ObservableCollection<string> documentOutline;

        public ObservableCollection<string> DocumentOutline {
            get {
                return documentOutline;
            }
        }

        public FountainOutlineViewModel(IMessagePublisher<DocumentMessage> documentMessagePublisher, 
            IMessagePublisher<DragDropMessage> dragDropMessagePublisher)
        {
            documentMessagePublisher.Subscribe(DocumentChanged);
            dragDropMessagePublisher.Subscribe(DragAndDrop);
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
            string dropItem = documentOutline.ElementAt(dropIndex).ToString();

            if (dragIndex < 0 | dropIndex < 0)
            {
                return;
            }

            documentOutline.RemoveAt(dragIndex);
            documentOutline.Insert(dropIndex, dragItem);

        }
    }
}
