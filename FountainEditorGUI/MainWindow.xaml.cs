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
        public static DependencyProperty ScriptTextProperty = DependencyProperty.Register("ScriptText", typeof(string), typeof(MainWindow));

        public ObservableCollection<Element> DocumentTree { get; set; }

        public ObservableCollection<Element> OutlineElements { get; set; }   

        public string ScriptText
        {
            get { return (string)GetValue(ScriptTextProperty); }
            set { SetValue(ScriptTextProperty, value); }
        }

        public static DependencyProperty DocumentNameProperty = DependencyProperty.Register("DocumentName", typeof(string), typeof(MainWindow));
        
        public string DocumentName
        {
            get { return (string)GetValue(DocumentNameProperty); }
            set { SetValue(DocumentNameProperty, value); }
        }
        
        public MainWindow()
        {
            OutlineElements = new ObservableCollection<Element>();
            InitializeComponent();
        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenItem_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text Documents (.txt)|*.txt|Fountain Documents(.fountain)|*.fountain";

            if (dlg.ShowDialog() == true)
            {
                var filename = dlg.FileName;
                var inputText = new StreamReader(dlg.FileName).ReadToEnd();

                var normalText = Normalizer.Normalize(inputText);
                var tree = Tokenizer.Parse(normalText);

                Optimizer.Optimize(tree);

                DocumentTree = new ObservableCollection<Element>(tree);
                ScriptText = DocumentTree.Aggregate("", (curr, next) => curr + next.Text);
                DocumentName = System.IO.Path.GetFileName(filename);

                foreach (var item in tree)
                {
                    if (item is OutlineTextElement || item is SynopsisTextElement)
                    {
                        OutlineElements.Add(item);
                    }
                }
                
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();

            dlg.FileName = DocumentName;
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text Documents (.txt)|*.txt| Fountain Documents (.fountain)|*.fountain";

            if (dlg.ShowDialog() == true)
            {
                var saveText = new StreamWriter(DocumentName);
                saveText.Write(ScriptText);
                DocumentName = System.IO.Path.GetFileName(dlg.FileName);
            }
        }
    }
}
