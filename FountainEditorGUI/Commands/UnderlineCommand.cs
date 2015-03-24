using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using FountainEditor.Messaging;
using FountainEditorGUI.Messages;

namespace FountainEditorGUI.Commands
{
    public sealed class UnderlineCommand : CommandBase<RichTextBox>
    {
        private ITextCounter counter;
        private MarkdownFormatter formatter;
        private TextTrimmerService trimmer;
        private MarkdownUnderlineFormat underlineFormat;
        private TextScanner textScanner;
        private IMessagePublisher<ParagraphMessage> paragraphMessage;

        public UnderlineCommand(
            ITextCounter counter, 
            MarkdownFormatter formatter,
            TextTrimmerService trimmer,
            MarkdownUnderlineFormat underlineFormat,
            TextScanner textScanner,
            IMessagePublisher<ParagraphMessage> ParagraphMessage)
        {
            this.counter = counter;
            this.formatter = formatter;
            this.trimmer = trimmer;
            this.underlineFormat = underlineFormat;
            this.textScanner = textScanner;
            this.paragraphMessage = ParagraphMessage;
        }

        public override void Execute(RichTextBox parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }

            TextSelection selection = parameter.Selection;

            if (selection == null)
            {
                return;
            }

            TextRange selectionRange = new TextRange(selection.Start, selection.End);

            if (selectionRange.GetPropertyValue(Inline.TextDecorationsProperty).Equals(TextDecorations.Underline))
            {
                selectionRange.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                var range = counter.CountMarkdownSymbols(selectionRange.Text);

                selection.Text = trimmer.TrimText(selection.Text, range.Start, range.End);
                selection.Text = formatter.format(new TextRange(selection.Start, selection.End)); 
            }
            else
            {
                selection.Text = underlineFormat.formatMarkdown(selection.Text);
                selectionRange.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }

            string text = textScanner.ScanForText(selection.Start);
            paragraphMessage.Publish(new ParagraphMessage(text));
        }
    }
}
