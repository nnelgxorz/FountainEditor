using FountainEditor.Messaging;
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
    public sealed class BoneyardCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private MarkdownBoneyardFormat boneyardFormat;
        private TextTrimmerService trimmer;
        private IMessagePublisher<ParagraphMessage> paragraphMessagePublisher;

        public BoneyardCommand(MarkdownBoneyardFormat boneyardFormat, TextTrimmerService trimmer,
                               IMessagePublisher<ParagraphMessage> paragraphMessagePublisher)
        {
            this.boneyardFormat = boneyardFormat;
            this.trimmer = trimmer;
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

            if (richTextBox == null | selection == null)
            {
                return;
            }
            if (selection.Text.StartsWith("/*"))
            {
                selection.Text = trimmer.TrimText(selection.Text, 2, 2);
            }
            else
            {
                selection.Text = boneyardFormat.formatMarkdown(selection.Text);
            }

            paragraphMessagePublisher.Publish(new ParagraphMessage(selection.Text));
        }
    }
}
