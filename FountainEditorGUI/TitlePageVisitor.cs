using System.Windows.Documents;
using System.Windows.Media;

namespace FountainEditorGUI
{
    //class TitlePageVisitor : FountainEditor.TitlePageBaseVisitor<Block>
    //{
    //    public override Block VisitPair(FountainEditor.TitlePageParser.PairContext context)
    //    {
    //        Span s = new Span();
    //        Paragraph p = new Paragraph();
    //        Run kR = new Run(context.key().GetText());
    //        kR.Foreground = Brushes.Gray;
    //        s.Inlines.Add(kR);

    //        var value = context.value().GetText();
    //        s.Inlines.Add(ParseMarkdown.Parse(context.value().GetText()));
    //        p.Inlines.Add(s);
    //        return p;
    //    }

    //    public override Block VisitKey(FountainEditor.TitlePageParser.KeyContext context)
    //    {
    //        Paragraph p = new Paragraph();
    //        Run text = new Run(context.GetText());
    //        text.Foreground = Brushes.Gray;
    //        p.Inlines.Add(text);
    //        return p;
    //    }

    //    public override Block VisitValue(FountainEditor.TitlePageParser.ValueContext context)
    //    {
    //        Paragraph p = new Paragraph();
    //        Run text = new Run(context.GetText());
    //        text.Foreground = Brushes.Gray;
    //        p.Inlines.Add(text);
    //        return p;
    //    }
    //}
}
