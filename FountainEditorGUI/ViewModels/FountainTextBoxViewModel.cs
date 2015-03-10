using System.Windows.Documents;
using FountainEditor;
using FountainEditor.Messaging;
using FountainEditorGUI.Messages;
using FountainEditor.Language;
using FountainEditorGUI.Commands;
using System.Windows.Input;

namespace FountainEditorGUI.ViewModels
{
    public sealed class FountainTextBoxViewModel : ViewModelBase
    {
        private FlowDocument document;
        private IFountainService fountainService;
        private IMessagePublisher<DocumentMessage> documentMessagePublisher;

        public ICommand BoldCommand { get; set; }
        public ICommand ItalicsCommand { get; set; }
        public ICommand UnderlineCommand { get; set; }

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

        public FountainTextBoxViewModel(IFountainService fountainService, IMessagePublisher<DocumentMessage> documentMessagePublisher,
            IMessagePublisher<TextChangedMessage> textChangedMessagePublisher)
        {
            this.fountainService = fountainService;
            this.documentMessagePublisher = documentMessagePublisher;
            BoldCommand = new BoldCommand();
            ItalicsCommand = new ItalicsCommand();
            UnderlineCommand = new UnderlineCommand();

            documentMessagePublisher.Subscribe(DocumentChanged);
            textChangedMessagePublisher.Subscribe(TextChanged);
        }

        private void TextChanged(TextChangedMessage obj)
        {
            var elements = fountainService.Parse(obj.Text);
            documentMessagePublisher.Publish(new DocumentMessage(elements, ""));
        }

        private void DocumentChanged(DocumentMessage message)
        {
            var visitor = new DocumentVisitor();

            visitor.VisitAll(message.Document);

            this.Document = visitor.Document;
        }
    }
}
