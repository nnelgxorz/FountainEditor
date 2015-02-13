using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using FountainEditor.Language;
using FountainEditor.Messaging;
using FountainEditor.ObjectModel;
using FountainEditorGUI.Commands;
using FountainEditorGUI.Messages;
using Microsoft.Win32;

namespace FountainEditorGUI.ViewModels
{
    public sealed class MainWindowViewModel : ViewModelBase
    {
        private IFountainService fountainService;
        private IMessagePublisher<DocumentMessage> documentMessagePublisher;
        private Element[] document;
        private string documentName;

        public Element[] Document
        {
            get { return this.document; }
            set
            {
                if (this.document != value)
                {
                    this.document = value;
                    
                    OnDocumentChanged();
                }
            }
        }

        public ICommand NewCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public ICommand SaveAsCommand { get; private set; }

        public MainWindowViewModel(IFountainService fountainService, IMessagePublisher<DocumentMessage> documentMessagePublisher)
        {
            NewCommand = new RelayCommand(New);
            OpenCommand = new RelayCommand(Open);
            SaveAsCommand = new RelayCommand(SaveAs);
            ExitCommand = new RelayCommand(Exit);

            this.fountainService = fountainService;
            this.documentMessagePublisher = documentMessagePublisher;
        }

        private void New()
        {
            this.document = new Element[0];
            this.documentName = "<Untitled>";

            this.OnDocumentChanged();
        }

        private void Open()
        {
            var openDialog = new OpenFileDialog();

            openDialog.DefaultExt = "*.txt";
            openDialog.Filter = "Text Documents (*.txt)|*.txt|Fountain Documents (*.fountain)|*.fountain";

            if (openDialog.ShowDialog() == true)
            {
                this.document = fountainService.ParseFile(openDialog.FileName);
                this.documentName = Path.GetFileNameWithoutExtension(openDialog.FileName);

                this.OnDocumentChanged();
            }
        }

        private void SaveAs()
        {
            var documentText = this.Document.ToString();
            var saveDialog = new SaveFileDialog();

            saveDialog.DefaultExt = "*.txt";
            saveDialog.OverwritePrompt = true;

            saveDialog.Filter = "Text Documents (*.txt)|*.txt|Fountain Documents (*.fountain)|*.fountain";

            if (saveDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveDialog.FileName, documentText, Encoding.UTF8);

                this.documentName = Path.GetFileNameWithoutExtension(saveDialog.FileName);

                this.OnDocumentChanged();
            }
        }

        private void Exit()
        {
            Application.Current.MainWindow.Close();
        }

        private void OnDocumentChanged() {
            documentMessagePublisher.Publish(new DocumentMessage(document, documentName));
        }
    }
}
