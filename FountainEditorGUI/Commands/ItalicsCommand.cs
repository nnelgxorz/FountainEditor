﻿using FountainEditor.Messaging;
using FountainEditorGUI.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace FountainEditorGUI.Commands
{
    public sealed class ItalicsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private CountMarkdownBackward countBackward;
        private CountMarkdownForward countForward;
        private MarkdownFormatter formatter;
        private TextTrimmerService trimmer;
        private TextScanner textScanner;
        private IMessagePublisher<ParagraphMessage> paragraphMessagePublisher;

        public ItalicsCommand(CountMarkdownBackward countbackward,
                            CountMarkdownForward countforward, MarkdownFormatter formatter,
                            TextTrimmerService trimmer, TextScanner textScanner,
                            IMessagePublisher<ParagraphMessage> paragraphMessagePublisher)
        {
            this.countBackward = countbackward;
            this.countForward = countforward;
            this.formatter = formatter;
            this.trimmer = trimmer;
            this.textScanner = textScanner;
            this.paragraphMessagePublisher = paragraphMessagePublisher;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            RichTextBox richTextBox = parameter as RichTextBox;
            TextSelection selection = richTextBox.Selection;
            TextRange selectionRange = new TextRange(selection.Start, selection.End);
            
            if (richTextBox == null || selection == null)
            {
                return;
            }

            if (selection.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic))
            {
                selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            }
            else
            {
                selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }

            int start = countForward.Count(selectionRange.Text);
            int end = countBackward.Count(selectionRange.Text);

            selection.Text = trimmer.TrimText(selection.Text, start, end);
            selection.Text = formatter.format(new TextRange(selection.Start, selection.End));

            string text = textScanner.ScanForText(selection.Start);
            paragraphMessagePublisher.Publish(new ParagraphMessage(text));
        }
    }
}
