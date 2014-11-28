using System;
using System.Collections.Generic;
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

namespace FountainEditorGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<FountainEditor.Element> DocumentTree
        {
            get;
            set;
        }

        public static DependencyProperty ScriptTextProperty = DependencyProperty.Register("ScriptText", typeof(string), typeof(MainWindow));

        public string ScriptText
        {
            get { return (string)GetValue(ScriptTextProperty); }
            set { SetValue(ScriptTextProperty, value); }
        }

        public static DependencyProperty OutlineElementProperty = DependencyProperty.Register("OutlineElement", typeof(List<Element>), typeof(MainWindow));
        
        public List<Element> OutlineElements
        {
            get { return (List<Element>)GetValue(OutlineElementProperty); }
            set { SetValue(OutlineElementProperty, value); }
        }

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
            Microsoft.Win32.OpenFileDialog dlg = new
            Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text Documents (.txt)|*.txt| Fountain Documents (.fountain)|*.fountain";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                var filename = dlg.FileName;
                var inputText = new StreamReader(dlg.FileName).ReadToEnd();

                var normalText = Normalizer.Normalize(inputText);
                var tree = Tokenizer.Parse(normalText);
                Optimizer.Optimize(tree);

                DocumentTree = tree;
                ScriptText = DocumentTree.Aggregate("", (curr, next) => curr + next.Print());
                OutlineElements = DocumentTree;
                DocumentName = System.IO.Path.GetFileName(filename);
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new
            Microsoft.Win32.SaveFileDialog();

            dlg.FileName = DocumentName;
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text Documents (.txt)|*.txt| Fountain Documents (.fountain)|*.fountain";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                //var saveText = new StreamWriter();
                DocumentName = dlg.FileName;
            }

        }
    }
}
