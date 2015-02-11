using System;
using System.Windows.Documents;

namespace FountainEditorGUI {
    public sealed class DocumentService : IDocumentService {
        private MainWindow mainWindow;

        public DocumentService(MainWindow mainWindow) {
            this.mainWindow = mainWindow;
        }

        public DocumentService(Func<FlowDocument> getter, Action<FlowDocument> setter) {
        }

        public FlowDocument GetValue() {
            return mainWindow.DisplayBox.Document;
        }

        public void SetValue(FlowDocument value) {
            mainWindow.DisplayBox.Document = value;
        }
    }
}
