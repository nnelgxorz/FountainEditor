using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public sealed class GetTextPointerAtOffsetService
    {
        public TextPointer GetTextPointer (RichTextBox TextBox, int offset )
        {
            var navigator = TextBox.Document.ContentStart;
            int count = 0;

            while (navigator.CompareTo(TextBox.Document.ContentEnd) < 0)
            {
                TextPointerContext context = navigator.GetPointerContext(LogicalDirection.Backward);

                Run run = navigator.Parent as Run;

                if (context == TextPointerContext.ElementStart && run != null)
                {
                    string runText = run.Text;
                }

                navigator = navigator.GetNextContextPosition(LogicalDirection.Forward);
            }
            return navigator;
        }
    }
}
