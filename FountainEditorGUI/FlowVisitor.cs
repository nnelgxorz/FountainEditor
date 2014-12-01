using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    class FlowVisitor : FountainEditor.FountainBaseListener
    {
        public FlowDocument displayDoc = new  FlowDocument();
        public override void EnterSection(FountainEditor.FountainParser.SectionContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();

            p.Margin = new System.Windows.Thickness(100, 0, 50, 0);
            p.Foreground = System.Windows.Media.Brushes.Gray;
            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
        }

        public override void EnterSynopsis(FountainEditor.FountainParser.SynopsisContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();

            p.Margin = new System.Windows.Thickness(100, 0, 50, 0);
            p.Foreground = System.Windows.Media.Brushes.Gray;
            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
        }

        public override void EnterHeading(FountainEditor.FountainParser.HeadingContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();

            p.FontWeight = System.Windows.FontWeights.Bold;

            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
        }

        public override void EnterTransition(FountainEditor.FountainParser.TransitionContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();

            p.Foreground = System.Windows.Media.Brushes.Black;
            p.TextAlignment = System.Windows.TextAlignment.Right;
            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
        }

        public override void EnterCharacter(FountainEditor.FountainParser.CharacterContext context)

        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();

            p.Margin = new System.Windows.Thickness(270, 0, 230, 0);
            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);

            foreach (var item in context.children.Skip(1))
            {
                string dialogueText = context.GetText();
                Run run = new Run(dialogueText);
                Paragraph paragraph = new Paragraph();

                //p.Margin = new System.Windows.Thickness(270, 0, 230, 0);
                p.Inlines.Add(r);
                displayDoc.Blocks.Add(p);
            }
        }

        public override void EnterLine(FountainEditor.FountainParser.LineContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();

            //p.Margin = new System.Windows.Thickness(100, 0, 100, 0);
            p.Foreground = System.Windows.Media.Brushes.Black;
            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
        }

        public override void EnterCentered(FountainEditor.FountainParser.CenteredContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();

            p.TextAlignment = System.Windows.TextAlignment.Center;
            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
        }

        public override void EnterEol(FountainEditor.FountainParser.EolContext context)
        {
            string text = "";
            Run r = new Run(text);
            Paragraph p = new Paragraph();

            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
        }
    }
}
