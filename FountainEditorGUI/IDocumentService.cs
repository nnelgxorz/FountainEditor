using System.Windows.Documents;

namespace FountainEditorGUI {
    public interface IDocumentService {
        FlowDocument GetValue();

        void SetValue(FlowDocument value);
    }
}
