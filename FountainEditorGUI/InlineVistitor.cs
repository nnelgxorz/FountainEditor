using FountainEditor;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Linq;

namespace FountainEditorGUI
{
    class InlineVistitor : MarkdownBaseVisitor<Inline>
    {
        public override Inline VisitCompileUnit(MarkdownParser.CompileUnitContext context)
        {
            var span = new Span();

            foreach (var child in context.children)
            {
                span.Inlines.Add(Visit(child));
            }

            return span;
        }

        public override Inline VisitItalics(MarkdownParser.ItalicsContext context)
        {
            var inline = Visit(context.md());
            inline.FontStyle = FontStyles.Italic;

            var span = new Span();
            span.Inlines.Add(new Run("*") { Foreground = Brushes.Gray });
            span.Inlines.Add(inline);
            span.Inlines.Add(new Run("*") { Foreground = Brushes.Gray });

            return span;
        }

        public override Inline VisitBold(MarkdownParser.BoldContext context)
        {
            var inline = Visit(context.md());
            inline.FontWeight = FontWeights.Bold;

            var span = new Span();
            span.Inlines.Add(new Run("**") { Foreground = Brushes.Gray });
            span.Inlines.Add(inline);
            span.Inlines.Add(new Run("**") { Foreground = Brushes.Gray });

            return span;
        }

        public override Inline VisitBoldItalics(MarkdownParser.BoldItalicsContext context)
        {
            var inline = Visit(context.md());
            inline.FontStyle = FontStyles.Italic;
            inline.FontWeight = FontWeights.Bold;

            var span = new Span();
            span.Inlines.Add(new Run("***") { Foreground = Brushes.Gray });
            span.Inlines.Add(inline);
            span.Inlines.Add(new Run("***") { Foreground = Brushes.Gray });

            return span;
        }

        public override Inline VisitUnderline(MarkdownParser.UnderlineContext context)
        {
            var inline = Visit(context.md());
            inline.TextDecorations = TextDecorations.Underline;

            var span = new Span();
            span.Inlines.Add(new Run("_") { Foreground = Brushes.Gray });
            span.Inlines.Add(inline);
            span.Inlines.Add(new Run("_") { Foreground = Brushes.Gray });

            return span;
        }

        public override Inline VisitBoneyard(MarkdownParser.BoneyardContext context)
        {
            var span = new Span();
            span.Inlines.Add(new Run(context.GetText()) { Foreground = Brushes.Gray });

            return span;
        }

        public override Inline VisitNotes(MarkdownParser.NotesContext context)
        {
            var span = new Span();
            span.Inlines.Add(new Run(context.GetText()) { Background = Brushes.Yellow });

            return span;
        }

        public override Inline VisitString(MarkdownParser.StringContext context)
        {
            return new Run(context.GetText());
        }
    }
}
