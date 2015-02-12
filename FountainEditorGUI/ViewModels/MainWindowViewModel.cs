using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using FountainEditor;
using FountainEditorGUI.Commands;
using Microsoft.Win32;

namespace FountainEditorGUI.ViewModels
{
    public sealed class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<string> outline;
        private string documentName;

        public ObservableCollection<string> Outline
        {
            get { return outline; }
            set
            {
                if (outline != value)
                {
                    outline = value;

                    OnPropertyChanged();
                }
            }
        }

        public string DocumentName {
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

        public ICommand NewCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public ICommand SaveAsCommand { get; private set; }

        public MainWindowViewModel(IDocumentService documentService)
        {
            NewCommand = new RelayCommand(New);
            OpenCommand = new RelayCommand(Open);
            SaveAsCommand = new RelayCommand(SaveAs);
            ExitCommand = new RelayCommand(Exit);

        }

        private void New()
        {
            this.DocumentName = "<Untitled>";
        }

        private void Open()
        {
            var openDialog = new OpenFileDialog();

            openDialog.DefaultExt = "*.txt";
            openDialog.Filter = "Text Documents (*.txt)|*.txt|Fountain Documents (*.fountain)|*.fountain";

            if (openDialog.ShowDialog() == true)
            {
                var fileName = openDialog.FileName;
                DocumentName = Path.GetFileNameWithoutExtension(fileName);

                var stream = new AntlrFileStream(fileName);
                var lexer = new FountainLexer(stream);
                var tokens = new CommonTokenStream(lexer);
                var parser = new FountainParser(tokens);
                var tree = parser.compileUnit();
                var treeWalker = new ParseTreeWalker();
                var visitor = new FlowVisitor();
                treeWalker.Walk(visitor, tree);

                this.Outline = visitor.DisplayOutline;
            }
        }

        private void SaveAs()
        {
            var saveDialog = new SaveFileDialog();

            saveDialog.DefaultExt = "*.txt";
            saveDialog.OverwritePrompt = true;

            saveDialog.Filter = "Text Documents (.txt)|*.txt|Fountain Documents(.fountain)|*.fountain";

            if (saveDialog.ShowDialog() == true)
            {
                var stream = new StreamWriter(saveDialog.FileName, false, Encoding.UTF8);

                stream.Write(documentText);
                stream.Close();

                DocumentName = Path.GetFileNameWithoutExtension(saveDialog.FileName);
            }
        }

        private void Exit()
        {
            Application.Current.MainWindow.Close();
        }
    }
}
