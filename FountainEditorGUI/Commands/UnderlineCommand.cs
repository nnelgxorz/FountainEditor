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
    class UnderlineCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var richTextBox = parameter as RichTextBox;
            var selection = richTextBox.Selection;
            TextRange selectionRange = new TextRange(selection.Start, selection.End);
            Object underline = selectionRange.GetPropertyValue(Inline.TextDecorationsProperty);
            
            if (richTextBox == null)
            {
                return;
            }

            if (underline.Equals(TextDecorations.Underline))
            {
                if (selection.Text.StartsWith("_") && selection.Text.EndsWith("_"))
                {
                    selection.Text = selection.Text.Substring(1, selection.Text.Length - 2);
                }
                else
                {
                    selection.Text = string.Format("_{0}_", selection.Text);
                }
                selectionRange.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            }
            else
            {
                selection.Text = string.Format("_{0}_", selection.Text);
                selectionRange.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                return;
            }
        }
    }
}
