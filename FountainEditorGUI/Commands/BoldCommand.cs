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
    class BoldCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var richTextBox = parameter as RichTextBox;
            var document = richTextBox.Document;
            var selection = richTextBox.Selection;

            if (richTextBox == null | selection == null)
            {
                return;
            }
            TextRange selectionRange = new TextRange(selection.Start, selection.End);
            var weight = selectionRange.GetPropertyValue(TextElement.FontWeightProperty);
            var style = selectionRange.GetPropertyValue(TextElement.FontStyleProperty);
            if (weight.Equals(FontWeights.Normal))
            {
                if (style.Equals(FontStyles.Italic))
                {
                    if (selection.Text.StartsWith("*") && selection.Text.EndsWith("*"))
                    {
                        selection.Text = selection.Text.Substring(1, selection.Text.Length - 2);
                    }
                    selection.Text = string.Format("***{0}***", selection.Text);
                }
                else
                {
                    selection.Text = string.Format("**{0}**", selection.Text);
                }
                selectionRange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }

            if (weight.Equals(FontWeights.Bold))
            {
                if (selection.Text.StartsWith("**") && selection.Text.EndsWith("**"))
                {
                    selection.Text = selection.Text.Substring(2, selection.Text.Length - 4);
                }
                else
                {
                    if (style.Equals(FontStyles.Italic))
                    {
                        selection.Text = string.Format("*{0}*", selection.Text);
                    }
                    else
                    {
                        selection.Text = string.Format("**{0}**", selection.Text);
                    }
                }
                selectionRange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
        }
    }
}
