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

        public override void EnterSection(FountainEditor.FountainParser.SectionContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();

            p.Margin = new System.Windows.Thickness(20, 0, 100, 20);
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

            p.Margin = new System.Windows.Thickness(40, 0, 100, 20);
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

            p.Margin = new System.Windows.Thickness(150, 0, 100, 20);
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
            //Character     370,0,100,0
            //Parenthetical 310,0,290,0
            //Dialogue      250,0,250,0
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();

            p.Margin = new System.Windows.Thickness(250, 0, 250, 0);
            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
        }

        public override void EnterUpperCaseLine(FountainEditor.FountainParser.UpperCaseLineContext context)
        {
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

            p.FontStyle = System.Windows.FontStyles.Italic;
            p.Foreground = System.Windows.Media.Brushes.Green;

            p.Margin = new System.Windows.Thickness(60, 0, 0, 20);
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
            p.Margin = new System.Windows.Thickness(150, 0, 100, 20);

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
    }
}
