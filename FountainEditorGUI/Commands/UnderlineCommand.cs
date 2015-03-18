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
    public sealed class UnderlineCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private CountMarkdownBackward countBackward;
        private CountMarkdownForward countForward;
        private MarkdownFormatter formatter;
        private TextTrimmerService trimmer;
        private MarkdownUnderlineFormat underlineFormat;

        public UnderlineCommand(CountMarkdownBackward countbackward, CountMarkdownForward countforward, 
                                MarkdownFormatter formatter, TextTrimmerService trimmer, MarkdownUnderlineFormat underlineFormat)
        {
            this.countBackward = countbackward;
            this.countForward = countforward;
            this.formatter = formatter;
            this.trimmer = trimmer;
            this.underlineFormat = underlineFormat;
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

            if (selectionRange.GetPropertyValue(Inline.TextDecorationsProperty).Equals(TextDecorations.Underline))
            {
                selectionRange.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                int start = countForward.Count(selectionRange.Text);
                int end = countBackward.Count(selectionRange.Text);

                selection.Text = trimmer.TrimText(selection.Text, start, end);
                selection.Text = formatter.format(new TextRange(selection.Start, selection.End)); 
            }
            else
            {
                selection.Text = underlineFormat.formatMarkdown(selection.Text);
                selectionRange.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }
    }
}
