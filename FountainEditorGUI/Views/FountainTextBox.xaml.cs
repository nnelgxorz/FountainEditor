using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using FountainEditor.Messaging;
using FountainEditorGUI.Messages;
using FountainEditorGUI.ViewModels;
using System.Linq;

namespace FountainEditorGUI.Views
{
    /// <summary>
    /// Interaction logic for FountainTextBox.xaml
    /// </summary>
    public partial class FountainTextBox : UserControl
    {
        private ITextScanner textScanner;
        private GetTextOffsetService getOffset;
        private TextBoxDropAfterLogic dropAfterLogic;
        private TextBoxDropNestedLogic dropNestedLogic;
        private TextBoxDropUnNestedLogic dropUnNestedLogic;
        private GetParagraphIndexFromText getTextIndex;
        private TextPointerFromTextService pointerFromText;
        private GetTextPointerFromBlockIndex getPointerFromIndex;
        private IMessagePublisher<TextChangedMessage> textChangedMessagePublisher;
        private IMessagePublisher<OutlinerNavigationMessage> navigationMessagePublisher;
        private IMessagePublisher<DragDropMessage> dragDropMessagePublisher;
        private IMessagePublisher<SetCursorMessage> setCursorMessagePublisher;

        public FountainTextBox(FountainTextBoxViewModel viewModel, IMessagePublisher<TextChangedMessage> textChangedMessagePublisher,
            ITextScanner textScanner, GetTextOffsetService getOffset, IMessagePublisher<OutlinerNavigationMessage> navigationMessagePublisher,
            IMessagePublisher<DragDropMessage> dragDropMessagePublisher, IMessagePublisher<SetCursorMessage> setCursorMessagePublisher,
            GetParagraphIndexFromText getTextIndex, TextPointerFromTextService pointerFromText, GetTextPointerFromBlockIndex getPointerFromIndex,
            TextBoxDropAfterLogic dropAfterLogic, TextBoxDropNestedLogic dropNestedLogic, TextBoxDropUnNestedLogic dropUnNestedLogic)
        {
            InitializeComponent();

            this.DataContext = viewModel;
            this.textScanner = textScanner;
            this.getOffset = getOffset;
            this.dropAfterLogic = dropAfterLogic;
            this.dropNestedLogic = dropNestedLogic;
            this.dropUnNestedLogic = dropUnNestedLogic;
            this.getTextIndex = getTextIndex;
            this.pointerFromText = pointerFromText;
            this.getPointerFromIndex = getPointerFromIndex;
            this.textChangedMessagePublisher = textChangedMessagePublisher;
            this.navigationMessagePublisher = navigationMessagePublisher;
            this.dragDropMessagePublisher = dragDropMessagePublisher;
            this.setCursorMessagePublisher = setCursorMessagePublisher;

            dragDropMessagePublisher.Subscribe(onOutlineDragDrop);
            navigationMessagePublisher.Subscribe(onNavigationChange);
            setCursorMessagePublisher.Subscribe(onSetCursorChanged);
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
            var caret = this.DisplayBox.CaretPosition;
            Paragraph p = caret.Paragraph;
            int offset = getOffset.GetOffset(p.ContentStart, caret);

            if (e.Key == Key.Enter)
            {
                return;
            }

            if (e.Key == Key.Space | e.Key == Key.OemPeriod)
            {
                var currentText = new TextRange(p.ContentStart, p.ContentEnd).Text;
                var parseText = new TextRange(this.DisplayBox.Document.ContentStart, this.DisplayBox.Document.ContentEnd).Text;
                textChangedMessagePublisher.Publish(new TextChangedMessage(parseText));

                int index = getTextIndex.getIndex(this.DisplayBox.Document, currentText);
                setCursorMessagePublisher.Publish(new SetCursorMessage(index, offset));
            }  
        }

        private void onOutlineDragDrop(DragDropMessage message)
        {
            if (message.dragItemDepth < message.dropItemDepth)
            {
                this.DisplayBox.Document = dropNestedLogic.Drop(this.DisplayBox, message);
            }
            if (message.dragItemDepth > message.dropItemDepth)
            {
                this.DisplayBox.Document = dropUnNestedLogic.Drop(this.DisplayBox, message);
            }
            else
            {
                this.DisplayBox.Document = dropAfterLogic.Drop(this.DisplayBox, message);
            }
        }

        private void onNavigationChange(OutlinerNavigationMessage message)
        {
            TextPointer start = pointerFromText.getPointer(this.DisplayBox.Document, message.text, false);

            if (DisplayBox.Focus() == false)
            {
                FocusManager.SetFocusedElement(this.DisplayBox.Parent, this.DisplayBox);
            }

            int offset = getOffset.GetOffset(this.DisplayBox.Document.ContentStart, start);
            this.DisplayBox.ScrollToVerticalOffset((double)offset);
            this.DisplayBox.Selection.Select(start, start);
        }

        private void onSetCursorChanged(SetCursorMessage message)
        {
            TextPointer pointer = getPointerFromIndex.getPointer(this.DisplayBox.Document, message.index);

            pointer = pointer.GetPositionAtOffset(message.offset);

            this.DisplayBox.Selection.Select(pointer, pointer);
            int offset = getOffset.GetOffset(this.DisplayBox.Document.ContentStart, pointer);
        }
    }
}
