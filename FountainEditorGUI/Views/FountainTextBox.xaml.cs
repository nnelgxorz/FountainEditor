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
        private GetTextPointerAtOffsetService pointerAtOffset;
        private IMessagePublisher<TextChangedMessage> textChangedMessagePublisher;
        private IMessagePublisher<OutlinerNavigationMessage> navigationMessagePublisher;
        private IMessagePublisher<DragDropMessage> dragDropMessagePublisher;
        private TextPointerFromTextService pointerFromText;
        private GetEndOfHierarchicalTextSection getEndOfHierarchy;
        private CountHashTags countHashTags;
        private GetTextElementIndex getTextIndex;
        private IMessagePublisher<SetCursorMessage> setCursorMessagePublisher;
        private GetTextPointerFromBlockIndex getPointerFromIndex;

        public FountainTextBox(FountainTextBoxViewModel viewModel, IMessagePublisher<TextChangedMessage> textChangedMessagePublisher,
            ITextScanner textScanner, GetTextOffsetService getOffset, IMessagePublisher<OutlinerNavigationMessage> navigationMessagePublisher,
            GetTextPointerAtOffsetService pointerAtOffset, IMessagePublisher<DragDropMessage> dragDropMessagePublisher, 
            TextPointerFromTextService pointerFromText, GetEndOfHierarchicalTextSection getEndOfHierarchy, CountHashTags countHashTags,
            GetTextElementIndex getTextIndex, IMessagePublisher<SetCursorMessage> setCursorMessagePublisher, GetTextPointerFromBlockIndex getPointerFromIndex)
        {
            InitializeComponent();

            this.DataContext = viewModel;
            this.textScanner = textScanner;
            this.getOffset = getOffset;
            this.pointerAtOffset = pointerAtOffset;
            this.pointerFromText = pointerFromText;
            this.getEndOfHierarchy = getEndOfHierarchy;
            this.countHashTags = countHashTags;
            this.getTextIndex = getTextIndex;
            this.textChangedMessagePublisher = textChangedMessagePublisher;
            this.navigationMessagePublisher = navigationMessagePublisher;
            this.dragDropMessagePublisher = dragDropMessagePublisher;
            this.setCursorMessagePublisher = setCursorMessagePublisher;
            this.getPointerFromIndex = getPointerFromIndex;

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
            int dragIndex = getTextIndex.getIndex(this.DisplayBox.Document, message.dragItem);

            TextPointer startSelection = pointerFromText.getPointer(this.DisplayBox.Document, message.dragItem, false);
            TextPointer endSelection = getEndOfHierarchy.getPointer(this.DisplayBox.Document, message.dragItemDepth, dragIndex);

            if (DisplayBox.Focus() == false)
            {
                FocusManager.SetFocusedElement(this.DisplayBox.Parent, this.DisplayBox);
            }

            this.DisplayBox.Selection.Select(startSelection, endSelection);
            this.DisplayBox.Cut();

            int dropIndex = getTextIndex.getIndex(this.DisplayBox.Document, message.dropItem);
            TextPointer drop = getEndOfHierarchy.getPointer(this.DisplayBox.Document, message.dropItemDepth, dropIndex);
            
            if (drop.CompareTo(this.DisplayBox.Document.ContentEnd) == 0)
            {
                this.DisplayBox.Document.Blocks.Add(new Paragraph());
            }
            
            this.DisplayBox.Selection.Select(drop, drop);
            this.DisplayBox.Paste();

            navigationMessagePublisher.Publish(new OutlinerNavigationMessage(message.dragItem));
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
        }
    }
}
