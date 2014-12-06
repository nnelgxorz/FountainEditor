using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    class DialogVisitor : FountainEditor.FountainBaseVisitor<object>
    {
        private FlowDocument displayDoc;

        public DialogVisitor(FlowDocument displayDoc)
        {
            // TODO: Complete member initialization
            this.displayDoc = displayDoc;
        }

        public override object VisitCharacter(FountainEditor.FountainParser.CharacterContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();
            p.Margin = new System.Windows.Thickness(370, 0, 100, 0);
            p.Inlines.Add(r);

            this.displayDoc.Blocks.Add(p);
            return null;
        }

        public override object VisitSpan(FountainEditor.FountainParser.SpanContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();
            p.Margin = new System.Windows.Thickness(250,0,250,0);
            p.Inlines.Add(r);

            this.displayDoc.Blocks.Add(p);
            return null;
        }

        public override object VisitParenthetical(FountainEditor.FountainParser.ParentheticalContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();
            p.Margin = new System.Windows.Thickness(310, 0, 290, 0);
            p.Inlines.Add(r);

            this.displayDoc.Blocks.Add(p);
            return null;
        }

        public override object VisitBlankLine(FountainEditor.FountainParser.BlankLineContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();
            p.TextAlignment = System.Windows.TextAlignment.Center;
            p.Inlines.Add(r);

            this.displayDoc.Blocks.Add(p);
            return null;
        }
    }
}
