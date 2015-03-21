using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using FountainEditor;
using FountainEditor.ObjectModel;
using FountainEditor.Language;

namespace FountainEditorGUI
{
    public sealed class DocumentVisitor : FountainVisitor
    {
        private IMarkdownService markdownservice;
        public FlowDocument Document { get; private set; }

        public DocumentVisitor(IMarkdownService markdownService)
        {
            Document = new FlowDocument();
            this.markdownservice = markdownService;
        }

        public override void Visit(SectionElement context)
        {
            var text = context.Text;
            var p = new Paragraph();
            p.Margin = new Thickness(20, 0, 100, 20);
            p.Foreground = Brushes.Gray;
            p.Inlines.Add(text);

            Document.Blocks.Add(p);
        }

        public override void Visit(SynopsisElement context)
        {
            var text = context.Text;
            var p = new Paragraph();

            p.Margin = new Thickness(40, 0, 100, 20);
            p.Foreground = Brushes.Gray;
            p.Inlines.Add(text);
            p.FontStyle = FontStyles.Italic;

            Document.Blocks.Add(p);
        }

        public override void Visit(SceneHeadingElement context)
        {
            var p = new Paragraph();
            p.Margin = new Thickness(150, 20, 100, 20);

            var text = context.Text;
            if (text.StartsWith("."))
            {
                text = text.Substring(1);

                p.Inlines.Add(new Run(".") { Foreground = Brushes.Gray });
                p.Inlines.Add(new Run(text.ToUpper()));
            }
            else
            {
                p.Inlines.Add(new Run(text.ToUpper()));
            }

            Document.Blocks.Add(p);
        }

        public override void Visit(TransitionElement context)
        {
            var p = new Paragraph();
            p.TextAlignment = TextAlignment.Right;
            p.Margin = new Thickness(150, 0, 100, 20);

            var text = context.Text;
            if (text.StartsWith(">"))
            {
                text = text.Substring(1);
                p.Inlines.Add(new Run(">") { Foreground = Brushes.Gray });
                p.Inlines.Add(text.ToUpper());
            }
            else
            {
                p.Foreground = Brushes.Black;
                p.Inlines.Add(text.ToUpper());
            }

            Document.Blocks.Add(p);
        }

        //public override void Visit(Dialog context)
        //{
        //    var visitor = new DialogVisitor();
        //
        //    DisplayDocument.Blocks.Add(visitor.VisitCharacter(context.character()));
        //
        //    foreach (var item in context.dialogBlock().children)
        //    {
        //        var node = visitor.Visit(item);
        //        if (node != null)
        //        {
        //            DisplayDocument.Blocks.Add(node);
        //        }
        //    }
        //
        //    var p = new Paragraph();
        //    p.Margin = new Thickness(370, 0, 100, 0);
        //    p.Inlines.Add(string.Empty);
        //
        //    DisplayDocument.Blocks.Add(p);
        //}

        public override void Visit(ActionElement context)
        {
            var p = new Paragraph();
            p.Margin = new Thickness(150, 0, 100, 0);

            var text = context.Text;
            if (text.StartsWith("!"))
            {
                p.Inlines.Add(new Run("!") { Foreground = Brushes.Gray });
                //p.Inlines.Add(this.markdownservice.Parse(text.Substring(1)));
            }
            else
            {
                //p.Inlines.Add(this.markdownservice.Parse(text));
                p.Inlines.Add(text);
            }

            Document.Blocks.Add(p);
            Document.Blocks.Add(new Paragraph { Margin = new Thickness(150, 0, 100, 0) });
        }

        public override void Visit(CenteredElement context)
        {
            var text = context.Text;

            var p = new Paragraph();
            p.TextAlignment = TextAlignment.Center;
            p.Inlines.Add(new Run(">") { Foreground = Brushes.Gray });
            //p.Inlines.Add(this.markdownservice.Parse(text.Substring(1, text.Length - 2)));
            p.Inlines.Add(text.Substring(1, text.Length -2));
            p.Inlines.Add(new Run("<") { Foreground = Brushes.Gray });
            Document.Blocks.Add(p);
        }

        public override void Visit(PageBreakElement context)
        {
            var p = new Paragraph();
            p.Margin = new Thickness(150, 0, 100, 66);
            p.TextAlignment = TextAlignment.Center;
            p.Foreground = Brushes.Gray;
            p.Inlines.Add(context.Text);

            Document.Blocks.Add(p);
        }

        //public override void Visit(TitlePage context)
        //{
        //    var text = context.Text;
        //    var p = ParseTitlePage.Parse(text);
        //    p.TextAlignment = TextAlignment.Right;
        //    p.Margin = new Thickness(150, 0, 100, 0);
        //
        //    displayDoc.Blocks.Add(p);
        //}
    }
}
