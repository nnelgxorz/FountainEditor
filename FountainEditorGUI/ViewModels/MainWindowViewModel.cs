using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using FountainEditor;
using FountainEditor.Messaging;
using FountainEditorGUI.Commands;
using FountainEditorGUI.Messages;
using Microsoft.Win32;

namespace FountainEditorGUI.ViewModels
{
    public sealed class MainWindowViewModel : ViewModelBase
    {
        private IMessagePublisher<DocumentMessage> documentMessagePublisher;
        private FlowDocument document;
        private string documentName;

        public FlowDocument Document
        {
            get { return this.document; }
            set
            {
                if (this.document != value)
                {
                    this.document = value;
                    this.documentMessagePublisher.Publish(new DocumentMessage(this.document, this.documentName));
                }
            }
        }

        public ICommand NewCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public ICommand SaveAsCommand { get; private set; }

        public MainWindowViewModel(IMessagePublisher<DocumentMessage> documentMessagePublisher)
        {
            NewCommand = new RelayCommand(New);
            OpenCommand = new RelayCommand(Open);
            SaveAsCommand = new RelayCommand(SaveAs);
            ExitCommand = new RelayCommand(Exit);

            this.documentMessagePublisher = documentMessagePublisher;
        }

        private void New()
        {
            this.documentName = "<Untitled>";
            this.Document = new FlowDocument();
        }

        private void Open()
        {
            var openDialog = new OpenFileDialog();

            openDialog.DefaultExt = "*.txt";
            openDialog.Filter = "Text Documents (*.txt)|*.txt|Fountain Documents (*.fountain)|*.fountain";

            if (openDialog.ShowDialog() == true)
            {
                var stream = new AntlrFileStream(openDialog.FileName);
                var lexer = new FountainLexer(stream);
                var tokens = new CommonTokenStream(lexer);
                var parser = new FountainParser(tokens);
                var tree = parser.compileUnit();
                var treeWalker = new ParseTreeWalker();
                var visitor = new FlowVisitor();
                treeWalker.Walk(visitor, tree);

                this.documentName = Path.GetFileNameWithoutExtension(openDialog.FileName);
                this.Document = visitor.DisplayDocument;
            }
        }

        private void SaveAs()
        {
            var documentText = this.Document.ToString();
            var saveDialog = new SaveFileDialog();

            saveDialog.DefaultExt = "*.txt";
            saveDialog.OverwritePrompt = true;

            saveDialog.Filter = "Text Documents (.txt)|*.txt|Fountain Documents(.fountain)|*.fountain";

            if (saveDialog.ShowDialog() == true)
            {
                var stream = new StreamWriter(saveDialog.FileName, false, Encoding.UTF8);

                stream.Write(documentText);
                stream.Close();

                // TODO: Document name is never published to subscribers here.
                // 

                this.documentName = Path.GetFileNameWithoutExtension(saveDialog.FileName);
            }
        }

        private void Exit()
        {
            Application.Current.MainWindow.Close();
        }
    }
}
