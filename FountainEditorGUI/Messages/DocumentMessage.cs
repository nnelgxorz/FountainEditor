using System.Collections.Generic;
using System.Windows.Documents;
using FountainEditor.ObjectModel;

namespace FountainEditorGUI.Messages
{
    public sealed class DocumentMessage
    {
        public Element[] Document { get; private set; }
        public string DocumentName { get; private set; }

        public DocumentMessage(Element[] document, string documentName)
        {
            this.Document = document;
            this.DocumentName = documentName;
        }
    }
}
