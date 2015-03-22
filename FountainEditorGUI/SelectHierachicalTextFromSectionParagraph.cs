using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    public sealed class SelectHierachicalTextFromSectionParagraph
    {
        private int dragIndex;
        private GetParagraphIndexFromText getTextIndex;
        private TextPointerFromTextService pointerFromText;
        private GetPointerAtEndOfSectionHierarchy getEndOfHierarchy;
        private MoveTextPointerToNextContext movePointerToNextContext;
        public SelectHierachicalTextFromSectionParagraph(GetParagraphIndexFromText getTextIndex, TextPointerFromTextService pointerFromText,
            GetPointerAtEndOfSectionHierarchy getEndOfHierarchy, MoveTextPointerToNextContext movePointerToNextContext)
        {
            this.getTextIndex = getTextIndex;
            this.pointerFromText = pointerFromText;
            this.getEndOfHierarchy = getEndOfHierarchy;
            this.movePointerToNextContext = movePointerToNextContext;
        }
        public TextRange Select(RichTextBox DisplayBox, string text, int depth)
        {
            dragIndex = getTextIndex.getIndex(DisplayBox.Document, text);
            TextPointer startSelection = pointerFromText.getPointer(DisplayBox.Document, text, false);
            TextPointer endSelection = getEndOfHierarchy.getPointer(DisplayBox.Document, depth, dragIndex);

            TextRange textRange = new TextRange(startSelection, endSelection);

            return textRange;
        }
    }
}
