using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FountainEditorGUI.ViewModels;

namespace FountainEditorGUI.Views
{
    /// <summary>
    /// Interaction logic for FountainTextBox.xaml
    /// </summary>
    public partial class FountainTextBox : UserControl
    {
        private int count = 0;

        public FountainTextBox()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var notifier = this.DataContext as INotifyPropertyChanged;
            if (notifier != null)
            {
                notifier.PropertyChanged += PropertyChanged;
            }
        }

        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Document")
            {
                this.DisplayBox.Document = ((FountainTextBoxViewModel)this.DataContext).Document;
            }
        }

        private void PressEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // FlowDocument current = DisplayBox.Document;
                // string input = new TextRange(current.ContentStart, current.ContentEnd).Text;

                // var stream = new Antlr4.Runtime.AntlrInputStream(input);
                // var lexer = new FountainLexer(stream);
                // var tokens = new Antlr4.Runtime.CommonTokenStream(lexer);
                // var parser = new FountainParser(tokens);
                // var tree = parser.compileUnit();
                // var treeWalker = new Antlr4.Runtime.Tree.ParseTreeWalker();
                // var visitor = new FlowVisitor();
                // treeWalker.Walk(visitor, tree);

                // this.DisplayBox.Document = visitor.DisplayDocument;
                // this.DisplayBox.CaretPosition = this.DisplayBox.CaretPosition.DocumentEnd;
                count = 0;
                return;
            }

            if (e.Key == Key.Space || e.Key == Key.OemPeriod)
            {
                count++;

                if (count >= 2)
                {
                    // var cursor = DisplayBox.CaretPosition;
                    // FlowDocument current = DisplayBox.Document;
                    // string input = new TextRange(current.ContentStart, current.ContentEnd).Text;
                    // 
                    // var stream = new Antlr4.Runtime.AntlrInputStream(input);
                    // var lexer = new FountainLexer(stream);
                    // var tokens = new Antlr4.Runtime.CommonTokenStream(lexer);
                    // var parser = new FountainParser(tokens);
                    // var tree = parser.compileUnit();
                    // var treeWalker = new Antlr4.Runtime.Tree.ParseTreeWalker();
                    // var visitor = new FlowVisitor();
                    // treeWalker.Walk(visitor, tree);
                    // 
                    // count = 0;
                    // this.DisplayBox.Document = visitor.DisplayDocument;
                    // this.DisplayBox.CaretPosition = DisplayBox.CaretPosition.DocumentEnd;
                    // return;
                }
            }
            else
            {
                count = 0;
            }
        }
    }
}
