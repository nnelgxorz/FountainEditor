using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    class FlowVisitor : FountainEditor.FountainBaseListener
    {
        public FlowDocument displayDoc = new  FlowDocument();
        public ObservableCollection<String> displayOutline = new ObservableCollection<String>();

        public override void EnterBlankLine(FountainEditor.FountainParser.BlankLineContext context)
        {
            string text = "";
            Run r = new Run(text);
            Paragraph p = new Paragraph();

            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
        }

        public override void EnterSection(FountainEditor.FountainParser.SectionContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();

            p.Margin = new System.Windows.Thickness(20, 0, 100, 0);
            p.Foreground = System.Windows.Media.Brushes.Gray;
            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
            displayOutline.Add(text);
        }

        public override void EnterSynopsis(FountainEditor.FountainParser.SynopsisContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();

            p.Margin = new System.Windows.Thickness(40, 0, 100, 0);
            p.Foreground = System.Windows.Media.Brushes.Gray;
            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
            displayOutline.Add(text);
        }

        public override void EnterHeading(FountainEditor.FountainParser.HeadingContext context)
        {
            string text = context.GetText();
            Run r = new Run(text.ToUpper());
            Paragraph p = new Paragraph();

            p.Margin = new System.Windows.Thickness(150, 0, 100, 0);
            p.FontWeight = System.Windows.FontWeights.Bold;

            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
        }

        public override void EnterTransition(FountainEditor.FountainParser.TransitionContext context)
        {
            string text = context.GetText();
            Run r = new Run(text.ToUpper());
            Paragraph p = new Paragraph();

            p.Foreground = System.Windows.Media.Brushes.Black;
            p.TextAlignment = System.Windows.TextAlignment.Right;
            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
            
        }

        public override void EnterCharacter(FountainEditor.FountainParser.CharacterContext context)
        {
            var visitor = new DialogVisitor(displayDoc);

            visitor.Visit(context);
            
            //context.RemoveLastChild();
            //string text = context.GetText();
            //Run r = new Run(text);
            //Paragraph p = new Paragraph();
            //p.Margin = new System.Windows.Thickness(250, 0, 250, 0);
            //p.Inlines.Add(r);
            //displayDoc.Blocks.Add(p);
        }

        public override void EnterUpperCaseLine(FountainEditor.FountainParser.UpperCaseLineContext context)
        {
            context.RemoveLastChild();
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();

            p.Margin = new System.Windows.Thickness(150, 0, 100, 0);
            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
        }

        public override void EnterNote(FountainEditor.FountainParser.NoteContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();
            r.Background = System.Windows.Media.Brushes.LightYellow;
            p.Background = System.Windows.Media.Brushes.White;
            p.Foreground = System.Windows.Media.Brushes.Gray;

            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
            displayOutline.Add(text);
        }

        public override void EnterSpan(FountainEditor.FountainParser.SpanContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();

            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
            p.Margin = new System.Windows.Thickness(150, 0, 100, 0);

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

        public override void EnterPageBreak(FountainEditor.FountainParser.PageBreakContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();

            p.Foreground = System.Windows.Media.Brushes.Gray;
            p.TextAlignment = System.Windows.TextAlignment.Center;
            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
        }

        public override void EnterTitlePage(FountainEditor.FountainParser.TitlePageContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();

            p.Foreground = System.Windows.Media.Brushes.Gray;
            p.TextAlignment = System.Windows.TextAlignment.Right;
            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
        }
    }
}
