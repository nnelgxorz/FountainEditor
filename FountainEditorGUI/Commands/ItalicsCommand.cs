using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using FountainEditor.Messaging;
using FountainEditorGUI.Messages;

namespace FountainEditorGUI.Commands
{
    public sealed class ItalicsCommand : CommandBase<RichTextBox>
    {
        private readonly ITextCounter counter;
        private readonly MarkdownFormatter formatter;
        private readonly TextTrimmerService trimmer;
        private readonly TextScanner textScanner;
        private readonly IMessagePublisher<ParagraphMessage> paragraphMessagePublisher;

        public ItalicsCommand(
            ITextCounter counter,
            MarkdownFormatter formatter,
            TextTrimmerService trimmer,
            TextScanner textScanner,
            IMessagePublisher<ParagraphMessage> paragraphMessagePublisher)
        {
            this.counter = counter;
            this.formatter = formatter;
            this.trimmer = trimmer;
            this.textScanner = textScanner;
            this.paragraphMessagePublisher = paragraphMessagePublisher;
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

            if (selection.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic))
            {
                selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            }
            else
            {
                selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }

            var range = counter.CountMarkdownSymbols(selection.Text);

            selection.Text = trimmer.TrimText(selection.Text, range.Start, range.End);
            selection.Text = formatter.format(new TextRange(selection.Start, selection.End));

            string text = textScanner.ScanForText(selection.Start);
            paragraphMessagePublisher.Publish(new ParagraphMessage(text));
        }
    }
}
