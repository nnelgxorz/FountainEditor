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
        private IMarkdownService markdownService;
        private IMessagePublisher<DocumentMessage> documentMessagePublisher;
        private IMessagePublisher<ParagraphMessage> paragraphMessagePublisher;

        public ICommand BoldCommand { get; set; }
        public ICommand ItalicsCommand { get; set; }
        public ICommand UnderlineCommand { get; set; }
        public ICommand BoneYardCommand { get; set; }

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
            IMessagePublisher<TextChangedMessage> textChangedMessagePublisher, BoldCommand boldCommand, ItalicsCommand italicCommand,
            UnderlineCommand underlineCommand, BoneyardCommand boneyardCommand)
        {
            this.fountainService = fountainService;
            this.documentMessagePublisher = documentMessagePublisher;
            this.BoldCommand = boldCommand;
            this.ItalicsCommand = italicCommand;
            this.UnderlineCommand = underlineCommand;
            this.BoneYardCommand = boneyardCommand;

            documentMessagePublisher.Subscribe(DocumentChanged);
            textChangedMessagePublisher.Subscribe(TextChanged);
        }

        private void TextChanged(TextChangedMessage message)
        {
            var elements = fountainService.Parse(message.Text);
            //paragraphMessagePublisher.Publish(new ParagraphMessage(elements);
            documentMessagePublisher.Publish(new DocumentMessage(elements, ""));
        }

        private void DocumentChanged(DocumentMessage message)
        {
            var visitor = new DocumentVisitor(markdownService);

            visitor.VisitAll(message.Document);

            this.Document = visitor.Document;
        }
    }
}
