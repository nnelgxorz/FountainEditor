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
    class ItalicsCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            RichTextBox richTextBox = parameter as RichTextBox;
            TextSelection selection = richTextBox.Selection;
            TextRange selectionRange = new TextRange(selection.Start, selection.End);
            Object weight = selectionRange.GetPropertyValue(TextElement.FontWeightProperty);
            Object style = selectionRange.GetPropertyValue(TextElement.FontStyleProperty);

            if (richTextBox == null)
            {
                return;
            }

            if (style.Equals(FontStyles.Normal))
            {
                if (weight.Equals(FontWeights.Bold))
                {
                    if (selection.Text.StartsWith("**") && selection.Text.EndsWith("**"))
                    {
                        selection.Text = selection.Text.Substring(2, selection.Text.Length - 4);
                    }
                    selection.Text = string.Format("***{0}***", selection.Text);
                }
                else
                {
                    selection.Text = string.Format("*{0}*", selection.Text);
                }

                selectionRange.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }

            if (style.Equals(FontStyles.Italic))
            {
                if (selection.Text.StartsWith("*") && selection.Text.EndsWith("*"))
                {
                    selection.Text = selection.Text.Substring(1, selection.Text.Length - 2);
                }
                else
                {
                    if (weight.Equals(FontWeights.Bold))
                    {
                        selection.Text = string.Format("**{0}**");
                    }
                    else
                    {
                        selection.Text = string.Format("*{0}*", selection.Text);
                    }
                }
                selectionRange.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            } 
        }
    }
}
