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
        public Span s = new Span();

        public override void EnterItalics(FountainEditor.MarkdownParser.ItalicsContext context)
        {
            string text = context.GetText();
            text = text.Substring(1);
            text = text.Substring(0, text.Length - 1);
            Run sR = new Run("*");
            sR.Foreground = System.Windows.Media.Brushes.Gray;
            Run r = new Run(text);
            r.FontStyle = System.Windows.FontStyles.Italic;
            Run eR = new Run("*");
            eR.Foreground = System.Windows.Media.Brushes.Gray;
            s.Inlines.Add(sR);
            s.Inlines.Add(r);
            s.Inlines.Add(eR);
        }

        public override void EnterBold(FountainEditor.MarkdownParser.BoldContext context)
        {
            string text = context.GetText();

            text = text.Substring(2);
            text = text.Substring(0, text.Length - 2);
            Run sR = new Run("**");
            sR.Foreground = System.Windows.Media.Brushes.Gray;
            Run r = new Run(text);
            r.FontWeight = System.Windows.FontWeights.Bold;
            Run eR = new Run("**");
            eR.Foreground = System.Windows.Media.Brushes.Gray;
            s.Inlines.Add(sR);
            s.Inlines.Add(r);
            s.Inlines.Add(eR);
        }

        public override void EnterBoldItalics(FountainEditor.MarkdownParser.BoldItalicsContext context)
        {
            string text = context.GetText();

            text = text.Substring(3);
            text = text.Substring(0, text.Length - 3);
            Run sR = new Run("***");
            sR.Foreground = System.Windows.Media.Brushes.Gray;
            Run r = new Run(text);
            r.FontStyle = System.Windows.FontStyles.Italic;
            r.FontWeight = System.Windows.FontWeights.Bold;
            Run eR = new Run("***");
            eR.Foreground = System.Windows.Media.Brushes.Gray;
            s.Inlines.Add(sR);
            s.Inlines.Add(r);
            s.Inlines.Add(eR);
        }

        public override void EnterUnderline(FountainEditor.MarkdownParser.UnderlineContext context)
        {
            string text = context.GetText();
            text = text.Substring(1);
            text = text.Substring(0, text.Length - 1);
            Run sR = new Run("_");
            sR.Foreground = System.Windows.Media.Brushes.Gray;
            Run r = new Run(text);
            r.TextDecorations = System.Windows.TextDecorations.Underline;
            Run eR = new Run("_");
            eR.Foreground = System.Windows.Media.Brushes.Gray;
            s.Inlines.Add(sR);
            s.Inlines.Add(r);
            s.Inlines.Add(eR);
        }

        public override void EnterBoneyard(FountainEditor.MarkdownParser.BoneyardContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            r.Foreground = System.Windows.Media.Brushes.Gray;
            s.Inlines.Add(r);
        }

        public override void EnterNotes(FountainEditor.MarkdownParser.NotesContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            r.Background = System.Windows.Media.Brushes.Yellow;
            s.Inlines.Add(r);
        }

        public override void EnterWords(FountainEditor.MarkdownParser.WordsContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            s.Inlines.Add(r);
        }

        public override void EnterBlank(FountainEditor.MarkdownParser.BlankContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            s.Inlines.Add(r);
        }
    }
}
