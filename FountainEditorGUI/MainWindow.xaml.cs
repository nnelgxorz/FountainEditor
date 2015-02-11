using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using FountainEditor;
using FountainEditorGUI.ViewModels;

namespace FountainEditorGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int count = 0;

        public MainWindow()
        {
            InitializeComponent();

            var documentService = new DocumentService(this);

            DataContext = new MainWindowViewModel(documentService);
        }

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
        }
    }
}
