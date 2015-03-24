using System;
using System.Windows.Controls;
using System.Windows.Documents;
using FountainEditor.Messaging;
using FountainEditorGUI.Messages;

namespace FountainEditorGUI.Commands
{
    public sealed class BoneyardCommand : CommandBase<RichTextBox>
    {
        private MarkdownBoneyardFormat boneyardFormat;
        private TextTrimmerService trimmer;
        private IMessagePublisher<ParagraphMessage> paragraphMessagePublisher;

        public BoneyardCommand(
            MarkdownBoneyardFormat boneyardFormat,
            TextTrimmerService trimmer,
            IMessagePublisher<ParagraphMessage> paragraphMessagePublisher)
        {
            this.boneyardFormat = boneyardFormat;
            this.trimmer = trimmer;
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
