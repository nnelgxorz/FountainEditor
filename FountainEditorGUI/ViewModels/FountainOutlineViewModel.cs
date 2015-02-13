using System.Collections.Generic;
using System.Linq;
using FountainEditor.Messaging;
using FountainEditor.ObjectModel;
using FountainEditorGUI.Messages;

namespace FountainEditorGUI.ViewModels
{
    public sealed class FountainOutlineViewModel : ViewModelBase
    {
        private Element[] document = new Element[0];

        public IEnumerable<string> DocumentOutline {
            get {
                return from element in document
                       where element is SectionElement || element is SynopsisElement
                       select element.Text;
            }
        }

        public FountainOutlineViewModel(IMessagePublisher<DocumentMessage> documentMessagePublisher)
        {
            documentMessagePublisher.Subscribe(DocumentChanged);
        }

        private void DocumentChanged(DocumentMessage message)
        {
            this.document = message.Document;

            OnPropertyChanged("DocumentOutline");
        }
    }
}
