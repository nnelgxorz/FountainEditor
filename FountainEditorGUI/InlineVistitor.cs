using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using FountainEditor;

namespace FountainEditorGUI
{
    //class InlineVistitor : MarkdownBaseVisitor<Inline>
    //{
    //    public override Inline VisitCompileUnit(MarkdownParser.CompileUnitContext context)
    //    {
    //        var span = new Span();

    //        foreach (var child in context.children)
    //        {
    //            span.Inlines.Add(Visit(child));
    //        }

    //        return span;
    //    }

    //    public override Inline VisitItalics(MarkdownParser.ItalicsContext context)
    //    {
    //        var span = new Span();
    //        span.Inlines.Add(new Run("*") { Foreground = Brushes.Gray });

    //        foreach (var node in context.md())
    //        {
    //            var inline = Visit(node);
    //            inline.FontStyle = FontStyles.Italic;
    //            span.Inlines.Add(inline);
    //        }

    //        span.Inlines.Add(new Run("*") { Foreground = Brushes.Gray });

    //        return span;
    //    }

    //    public override Inline VisitBold(MarkdownParser.BoldContext context)
    //    {

    //        var span = new Span();
    //        span.Inlines.Add(new Run("**") { Foreground = Brushes.Gray });

    //        foreach (var node in context.md())
    //        {
    //            var inline = Visit(node);
    //            inline.FontWeight = FontWeights.Bold;
    //            span.Inlines.Add(inline);
    //        }

    //        span.Inlines.Add(new Run("**") { Foreground = Brushes.Gray });

    //        return span;
    //    }

    //    public override Inline VisitBoldItalics(MarkdownParser.BoldItalicsContext context)
    //    {

    //        var span = new Span();
    //        span.Inlines.Add(new Run("***") { Foreground = Brushes.Gray });

    //        foreach (var node in context.md())
    //        {
    //            var inline = Visit(node);
    //            inline.FontStyle = FontStyles.Italic;
    //            inline.FontWeight = FontWeights.Bold;
    //            span.Inlines.Add(inline);
    //        }

    //        span.Inlines.Add(new Run("***") { Foreground = Brushes.Gray });

    //        return span;
    //    }

    //    public override Inline VisitUnderline(MarkdownParser.UnderlineContext context)
    //    {

    //        var span = new Span();
    //        span.Inlines.Add(new Run("_") { Foreground = Brushes.Gray });

    //        foreach (var node in context.md())
    //        {
    //            var inline = Visit(node);
    //            inline.TextDecorations = TextDecorations.Underline;
    //            span.Inlines.Add(inline);
    //        }

    //        span.Inlines.Add(new Run("_") { Foreground = Brushes.Gray });

    //        return span;
    //    }

    //    public override Inline VisitBoneyard(MarkdownParser.BoneyardContext context)
    //    {
    //        var span = new Span();
    //        span.Inlines.Add(new Run(context.GetText()) { Foreground = Brushes.Gray });

    //        return span;
    //    }

    //    public override Inline VisitNotes(MarkdownParser.NotesContext context)
    //    {
    //        var span = new Span();
    //        span.Inlines.Add(new Run(context.GetText()) { Background = Brushes.Yellow });

    //        return span;
    //    }

    //    public override Inline VisitString(MarkdownParser.StringContext context)
    //    {
    //        return new Run(context.GetText());
    //    }
    //}
}
