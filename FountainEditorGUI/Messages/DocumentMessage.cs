using System.Windows.Documents;

namespace FountainEditorGUI.Messages
{
    public sealed class DocumentMessage
    {
        public FlowDocument Document { get; private set; }
        public string DocumentName { get; private set; }

        public DocumentMessage(FlowDocument document, string documentName)
        {
            this.Document = document;
            this.DocumentName = documentName;
        }
    }
}
