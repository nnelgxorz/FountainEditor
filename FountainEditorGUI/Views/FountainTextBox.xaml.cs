using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using FountainEditor.Messaging;
using FountainEditorGUI.Messages;
using FountainEditorGUI.ViewModels;

namespace FountainEditorGUI.Views
{
    /// <summary>
    /// Interaction logic for FountainTextBox.xaml
    /// </summary>
    public partial class FountainTextBox : UserControl
    {
        private IMessagePublisher<TextChangedMessage> textChangedMessagePublisher;
        private int count = 0;
        private ITextScanner textScanner;

        public FountainTextBox(FountainTextBoxViewModel viewModel, IMessagePublisher<TextChangedMessage> textChangedMessagePublisher,
            ITextScanner textScanner)
        {
            InitializeComponent();

            this.DataContext = viewModel;
            this.textChangedMessagePublisher = textChangedMessagePublisher;
            this.textScanner = textScanner;
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
            int offset = start.GetOffsetToPosition(caret);

            if (e.Key == Key.Enter)
            {
                return;
            }

            if (e.Key == Key.Space | e.Key == Key.OemPeriod)
            {
                var buffer = textScanner.ScanForText(caret);
                textChangedMessagePublisher.Publish(new TextChangedMessage(buffer));

                this.DisplayBox.CaretPosition = this.DisplayBox.CaretPosition.GetPositionAtOffset(offset);
            }
        }
    }
}
