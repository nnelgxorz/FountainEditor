using FountainEditor.Messaging;
using FountainEditorGUI.Messages;
using FountainEditorGUI.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Collections.Generic;

namespace FountainEditorGUI.Views
{
    /// <summary>
    /// Interaction logic for FountainTextBox.xaml
    /// </summary>
    public partial class FountainTextBox : UserControl
    {
        private ITextScanner textScanner;
        private IMessagePublisher<TextChangedMessage> textChangedMessagePublisher;
        private GetTextOffsetService getOffset;
        private GetParagraphIndexFromText getTextIndex;
        private TextPointerFromTextService pointerFromText;
        private GetTextPointerFromBlockIndex getPointerFromIndex;
        private GenerateIndexListOfSectionElementsInDocument generateSectionIndexList;
        private List<SectionIndexClass> indices;
        private GetPointersAtStartAndEndOfSectionTextHierarchy getStartAndEndofSection;
        private DisplayBoxDragAndDropService DisplayBoxDragDrop;
        private IMessagePublisher<SetCursorMessage> setCursorMessagePublisher;


        public FountainTextBox(FountainTextBoxViewModel viewModel, 
            ITextScanner textScanner, 
            IMessagePublisher<TextChangedMessage> textChangedMessagePublisher,
            IMessagePublisher<OutlinerNavigationMessage> navigationMessagePublisher,
            IMessagePublisher<DragDropMessage> dragDropMessagePublisher, 
            IMessagePublisher<SetCursorMessage> setCursorMessagePublisher,
            IMessagePublisher<SectionIndicesChangedMessage> sectionIndicesChanged,
            GetTextOffsetService getOffset, 
            GetParagraphIndexFromText getTextIndex, 
            TextPointerFromTextService pointerFromText, 
            GetTextPointerFromBlockIndex getPointerFromIndex,
            GenerateIndexListOfSectionElementsInDocument generateSectionIndexList,
            GetPointersAtStartAndEndOfSectionTextHierarchy getStartAndEndofSection,
            DisplayBoxDragAndDropService DisplayBoxDragDrop)
        {
            InitializeComponent();

            this.DataContext = viewModel;
            this.textScanner = textScanner;
            this.textChangedMessagePublisher = textChangedMessagePublisher;
            this.getOffset = getOffset;
            this.getTextIndex = getTextIndex;
            this.pointerFromText = pointerFromText;
            this.getPointerFromIndex = getPointerFromIndex;
            this.generateSectionIndexList = generateSectionIndexList;
            this.getStartAndEndofSection = getStartAndEndofSection;
            this.DisplayBoxDragDrop = DisplayBoxDragDrop;
            this.setCursorMessagePublisher = setCursorMessagePublisher;

            dragDropMessagePublisher.Subscribe(onOutlineDragDrop);
            navigationMessagePublisher.Subscribe(onNavigationChange);
            setCursorMessagePublisher.Subscribe(onSetCursorChanged);
            sectionIndicesChanged.Subscribe(onSectionIndicesChanged);
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
                indices = generateSectionIndexList.Generate(this.DisplayBox.Document);
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

            if (e.Key == Key.RightShift)
            {
                string message = null;
                foreach (var index in indices)
                {
                    message = string.Format("{0}{1}, {2}, {3}\t{4}\r\n", message, index.index, index.blockAmount, index.hashCount, index.text);
                }
                MessageBox.Show(string.Format("Of {0} paragraphs, {1} are section headings.\r\n\r\n{2}", 
                    this.DisplayBox.Document.Blocks.Count, indices.Count, message));
            }
        }

        private void onOutlineDragDrop(DragDropMessage message)
        {
            this.DisplayBox.Document = DisplayBoxDragDrop.Drop(this.DisplayBox, indices, message);
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
        private void onSectionIndicesChanged(SectionIndicesChangedMessage message)
        {
            this.indices = message.indices;
        }
    }
}
