using System.Windows.Documents;
using FountainEditor;
using FountainEditor.Messaging;
using FountainEditorGUI.Messages;

namespace FountainEditorGUI.ViewModels
{
    public sealed class FountainTextBoxViewModel : ViewModelBase
    {
        private FlowDocument document;

        public FlowDocument Document
        {
            get { return document; }
            set
            {
                if (this.document != value)
                {
                    this.document = value;

                    OnPropertyChanged();
                }
            }
        }

        public FountainTextBoxViewModel(IMessagePublisher<DocumentMessage> documentMessagePublisher)
        {
            documentMessagePublisher.Subscribe(DocumentChanged);
        }

        private void DocumentChanged(DocumentMessage message)
        {
            var visitor = new DocumentVisitor();

            visitor.VisitAll(message.Document);

            this.Document = visitor.Document;
        }
    }
}
