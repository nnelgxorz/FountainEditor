using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FountainEditor;
using FountainEditor.Elements;
using System.IO;
using Microsoft.Win32;

namespace FountainEditorGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public FlowDocument DisplayText { get; set; }

        public static DependencyProperty DocumentNameProperty = DependencyProperty.Register("DocumentName", typeof(string), typeof(MainWindow));
        
        public string DocumentName
        {
            get { return (string)GetValue(DocumentNameProperty); }
            set { SetValue(DocumentNameProperty, value); }
        }
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenItem_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();

            dlg.DefaultExt = "*.txt";
            dlg.Filter = "Text Documents (.txt)|*.txt|Fountain Documents(.fountain)|*.fountain";

            if (dlg.ShowDialog() == true)
            {
                var fileName = dlg.FileName;
                DocumentName = System.IO.Path.GetFileName(fileName);
                var stream = new Antlr4.Runtime.AntlrFileStream(fileName);
                var lexer = new FountainLexer(stream);
                var tokens = new Antlr4.Runtime.CommonTokenStream(lexer);
                var parser = new FountainParser(tokens);
                var tree = parser.compileUnit();
                var treeWalker = new Antlr4.Runtime.Tree.ParseTreeWalker();
                var visitor = new FlowVisitor();
                treeWalker.Walk(visitor, tree);

                this.DisplayBox.Document = visitor.displayDoc;
                this.Outliner.ItemsSource = visitor.displayOutline;
            }
        }

        int count = 0;
        private void PressEnter(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                FlowDocument current = DisplayBox.Document;
                string input = new TextRange(current.ContentStart, current.ContentEnd).Text;

                var stream = new Antlr4.Runtime.AntlrInputStream(input);
                var lexer = new FountainLexer(stream);
                var tokens = new Antlr4.Runtime.CommonTokenStream(lexer);
                var parser = new FountainParser(tokens);
                var tree = parser.compileUnit();
                var treeWalker = new Antlr4.Runtime.Tree.ParseTreeWalker();
                var visitor = new FlowVisitor();
                treeWalker.Walk(visitor, tree);

                this.Outliner.ItemsSource = visitor.DisplayOutline;
                this.DisplayBox.Document = visitor.DisplayDocument;
                this.DisplayBox.CaretPosition = this.DisplayBox.CaretPosition.DocumentEnd;
                count = 0;
                return;
            }

            if (e.Key == Key.Space || e.Key == Key.OemPeriod)
            {
                count++;

                if (count >= 2)
                {
                    var cursor = DisplayBox.CaretPosition;
                    FlowDocument current = DisplayBox.Document;
                    string input = new TextRange(current.ContentStart, current.ContentEnd).Text;

                    var stream = new Antlr4.Runtime.AntlrInputStream(input);
                    var lexer = new FountainLexer(stream);
                    var tokens = new Antlr4.Runtime.CommonTokenStream(lexer);
                    var parser = new FountainParser(tokens);
                    var tree = parser.compileUnit();
                    var treeWalker = new Antlr4.Runtime.Tree.ParseTreeWalker();
                    var visitor = new FlowVisitor();
                    treeWalker.Walk(visitor, tree);

                    this.Outliner.ItemsSource = visitor.DisplayOutline;
                    count = 0;
                    this.DisplayBox.Document = visitor.DisplayDocument;
                    this.DisplayBox.CaretPosition = DisplayBox.CaretPosition.DocumentEnd;
                    return;
                }
            }

            else
            {
                count = 0;
            }
            return;
        }

        private void SaveAsItem_Click(object sender, RoutedEventArgs e)
        {
            var documentText = this.DisplayBox.Document.ToString();
            var dlgsave = new SaveFileDialog();
            dlgsave.DefaultExt = "*.txt";
            dlgsave.OverwritePrompt = true;

            dlgsave.Filter = "Text Documents (.txt)|*.txt|Fountain Documents(.fountain)|*.fountain";

            if (dlgsave.ShowDialog() == true)
            {
                {
                    var utf8 = new UTF8Encoding();
                    var stream = new System.IO.StreamWriter(dlgsave.FileName, false, utf8);
                    stream.Write(documentText);
                    stream.Close();
                    DocumentName = System.IO.Path.GetFileName(dlgsave.FileName);
                }
            }
        }

        private void NewItem_Click(object sender, RoutedEventArgs e)
        {
            this.DisplayBox.Document = new FlowDocument();
        }
    }
            
}
