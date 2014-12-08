using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FountainEditorGUI
{
    class InlineVistitor : FountainEditor.MarkdownBaseListener
    {
        public FlowDocument test = new FlowDocument();

        public override void EnterItalics(FountainEditor.MarkdownParser.ItalicsContext context)
        {
            string text = context.GetText();
            Paragraph p = new Paragraph();
            Run r = new Run(text);
            Span s = new Span(r);
            s.FontStyle = System.Windows.FontStyles.Italic;
            p.Inlines.Add(s);
            test.Blocks.Add(p);
        }

        public override void EnterBold(FountainEditor.MarkdownParser.BoldContext context)
        {
            string text = context.GetText();
            Paragraph p = new Paragraph();
            Run r = new Run(text);
            Span s = new Span(r);
            s.FontWeight = System.Windows.FontWeights.Bold;
            p.Inlines.Add(s);
            test.Blocks.Add(p);
        }

        public override void EnterBoldItalics(FountainEditor.MarkdownParser.BoldItalicsContext context)
        {
            string text = context.GetText();
            Paragraph p = new Paragraph();
            Run r = new Run(text);
            Span s = new Span(r);
            p.FontStyle = System.Windows.FontStyles.Italic;
            p.FontWeight = System.Windows.FontWeights.Bold;
            p.Inlines.Add(s);
            test.Blocks.Add(p);
        }

        public override void EnterUnderline(FountainEditor.MarkdownParser.UnderlineContext context)
        {
            string text = context.GetText();
            Paragraph p = new Paragraph();
            Run r = new Run(text);
            Span s = new Span(r);
            p.TextDecorations = System.Windows.TextDecorations.Underline;
            p.Inlines.Add(s);
            test.Blocks.Add(p);
        }

        public override void EnterBoneyard(FountainEditor.MarkdownParser.BoneyardContext context)
        {
            string text = context.GetText();
            Paragraph p = new Paragraph();
            Run r = new Run(text);
            Span s = new Span(r);
            p.Foreground = System.Windows.Media.Brushes.Gray;
            p.Inlines.Add(s);
            test.Blocks.Add(p);
        }

        public override void EnterNotes(FountainEditor.MarkdownParser.NotesContext context)
        {
            string text = context.GetText();
            Paragraph p = new Paragraph();
            Run r = new Run(text);
            Span s = new Span(r);
            r.Background = System.Windows.Media.Brushes.Gold;
            p.Inlines.Add(s);
            test.Blocks.Add(p);
        }

        public override void EnterWords(FountainEditor.MarkdownParser.WordsContext context)
        {
            string text = context.GetText();
            Paragraph p = new Paragraph();
            Run r = new Run(text);
            p.Inlines.Add(r);
            test.Blocks.Add(p);
        }

        public override void EnterBlank(FountainEditor.MarkdownParser.BlankContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Paragraph p = new Paragraph();
            p.Inlines.Add(r);
            test.Blocks.Add(p);
        }
    }
}
