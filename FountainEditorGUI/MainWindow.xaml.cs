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

namespace FountainEditorGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DependencyProperty ScriptTextProperty = DependencyProperty.Register("ScriptText", typeof(string), typeof(MainWindow));

        public string ScriptText {
            get { return (string)GetValue(ScriptTextProperty); }
            set { SetValue(ScriptTextProperty, value); }
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
