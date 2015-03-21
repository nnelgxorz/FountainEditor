using System.Collections.Generic;
using System.Windows.Documents;
using FountainEditor.ObjectModel;

namespace FountainEditorGUI.Messages
{
    public sealed class ParagraphMessage
    {
        public string Paragraph { get; private set; }
        
        public ParagraphMessage(string paragraph)
        {
            this.Paragraph = paragraph;
        }
    }
}
