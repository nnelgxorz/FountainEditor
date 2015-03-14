using System.Windows.Controls;
using FountainEditorGUI.ViewModels;

namespace FountainEditorGUI.Views
{
    /// <summary>
    /// Interaction logic for FountainNameBox.xaml
    /// </summary>
    public partial class FountainNameBox : UserControl
    {
        public FountainNameBox(FountainNameBoxViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
