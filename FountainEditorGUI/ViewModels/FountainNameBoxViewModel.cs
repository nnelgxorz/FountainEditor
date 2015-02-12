using FountainEditor.Messaging;
using FountainEditorGUI.Messages;

namespace FountainEditorGUI.ViewModels
{
    public sealed class FountainNameBoxViewModel : ViewModelBase
    {
        private string documentName;

        public string DocumentName
        {
            get { return documentName; }
            set
            {
                if (documentName != value)
                {
                    documentName = value;

                    OnPropertyChanged();
                }
            }
        }

        public FountainNameBoxViewModel(IMessagePublisher<DocumentMessage> documentMessagePublisher)
        {
            documentMessagePublisher.Subscribe(DocumentChanged);
        }

        private void DocumentChanged(DocumentMessage message)
        {
            this.DocumentName = message.DocumentName;
        }
    }
}
