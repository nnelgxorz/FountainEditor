﻿using System.Windows.Controls;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public sealed class TextScanner : ITextScanner
    {
        public string ScanForText(TextPointer textPointer)
        {
            if (textPointer.GetPointerContext(LogicalDirection.Backward) == TextPointerContext.Text &&
                textPointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
            {
                return textPointer.GetTextInRun(LogicalDirection.Backward) + textPointer.GetTextInRun(LogicalDirection.Forward);
            }
            if (textPointer.GetPointerContext(LogicalDirection.Backward) == TextPointerContext.ElementStart)
            {
                return textPointer.GetTextInRun(LogicalDirection.Forward);
            }
            if (textPointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.ElementEnd)
            {
                return textPointer.GetTextInRun(LogicalDirection.Backward);
            }
            return string.Empty;
        }
    }
}
