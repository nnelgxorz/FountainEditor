using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FountainEditorGUI.ViewModels;
using FountainEditor.Language;
using System.Windows.Documents;
using FountainEditor.Messaging;
using FountainEditorGUI.Messages;

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
            TextPointer start = this.DisplayBox.Document.ContentStart;
            TextPointer caret = this.DisplayBox.CaretPosition;
            string buffer = this.DisplayBox.CaretPosition.GetTextInRun(LogicalDirection.Backward);

            int offset = start.GetOffsetToPosition(caret);

            if (e.Key == Key.Space)
            {
                if (buffer == null)
                {
                    return;
                }
                else
                {
                    var textChangedMessagePublisher = ServiceLocator.Current.GetInstance<IMessagePublisher<TextChangedMessage>>();
                    textChangedMessagePublisher.Publish(new TextChangedMessage(buffer));
                }
            }
        }
    }
}
