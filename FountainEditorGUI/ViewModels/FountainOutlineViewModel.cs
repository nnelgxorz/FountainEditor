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
        public ObservableCollection<string> documentOutline;
        private OutlinerDragDropLogic dragDropLogic;

        public ObservableCollection<string> DocumentOutline {
            get {
                return documentOutline;
            }
        }

        public FountainOutlineViewModel(IMessagePublisher<DocumentMessage> documentMessagePublisher, 
            IMessagePublisher<DragDropMessage> dragDropMessagePublisher, 
            OutlinerDragDropLogic dragDropLogic)
        {
            this.dragDropLogic = dragDropLogic;
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
            documentOutline = dragDropLogic.DoDrop(documentOutline, message);
        }
    }
}
 