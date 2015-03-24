using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using FountainEditor.Messaging;
using FountainEditorGUI.Messages;

namespace FountainEditorGUI.Commands
{
    public sealed class BoldCommand : CommandBase<RichTextBox>
    {
        private readonly ITextCounter counter;
        private readonly MarkdownFormatter formatter;
        private readonly TextTrimmerService trimmer;
        private readonly TextScanner textScanner;
        private readonly IMessagePublisher<ParagraphMessage> paragraphMessagePublisher;

        public BoldCommand(
            ITextCounter counter,
            MarkdownFormatter formatter, 
            TextTrimmerService trimmer,
            TextScanner textscanner, 
            IMessagePublisher<ParagraphMessage> paragraphMessagePublisher)
        {
            this.counter = counter;
            this.formatter = formatter;
            this.trimmer = trimmer;
            this.textScanner = textscanner;
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

            if (selectionRange.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold))
            {
                selectionRange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
            else
            {
                selectionRange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }

            var range = counter.CountMarkdownSymbols(selectionRange.Text);

            selection.Text = trimmer.TrimText(selection.Text, range.Start, range.End);
            selection.Text = formatter.format(new TextRange(selection.Start, selection.End));

            string text = textScanner.ScanForText(selection.Start);
            paragraphMessagePublisher.Publish(new ParagraphMessage(text));
        }
    }
}
