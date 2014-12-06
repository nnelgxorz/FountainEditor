using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    class DialogVisitor : FountainEditor.FountainBaseVisitor<Paragraph>
    {
        public override Paragraph VisitCharacter(FountainEditor.FountainParser.CharacterContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();
            p.Margin = new System.Windows.Thickness(370, 0, 100, 0);
            p.Inlines.Add(r);
            return p;
        }

        public override Paragraph VisitSpan(FountainEditor.FountainParser.SpanContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();
            p.Margin = new System.Windows.Thickness(250, 0, 250, 0);
            p.Inlines.Add(r);
            return p;
        }

        public override Paragraph VisitParenthetical(FountainEditor.FountainParser.ParentheticalContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();
            p.Margin = new System.Windows.Thickness(310, 0, 290, 0);
            p.Inlines.Add(r);
            return p;
        }
    }
}
