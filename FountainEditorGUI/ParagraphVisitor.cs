using FountainEditor.Language;
using FountainEditor.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace FountainEditorGUI
{
    public sealed class ParagraphVisitor : FountainVisitor
    {
        private IMarkdownService markdownservice;
        public Paragraph paragraph { get; private set; }

        public ParagraphVisitor(IMarkdownService markdownService)
        {
            paragraph = new Paragraph();
            this.markdownservice = markdownService;
        }

        public override void Visit(SectionElement context)
        {
            var text = context.Text;
            paragraph.Margin = new Thickness(20, 0, 100, 20);
            paragraph.Foreground = Brushes.Gray;
            paragraph.Inlines.Add(text);
        }

        public override void Visit(SynopsisElement context)
        {
            var text = context.Text;

            paragraph.Margin = new Thickness(40, 0, 100, 20);
            paragraph.Foreground = Brushes.Gray;
            paragraph.Inlines.Add(text);
            paragraph.FontStyle = FontStyles.Italic;
        }

        public override void Visit(SceneHeadingElement context)
        {
            var text = context.Text;
            if (text.StartsWith("."))
            {
                text = text.Substring(1);

                paragraph.Inlines.Add(new Run(".") { Foreground = Brushes.Gray });
                paragraph.Inlines.Add(new Run(text.ToUpper()));
            }
            else
            {
                paragraph.Inlines.Add(new Run(text.ToUpper()));
            }
        }

        public override void Visit(TransitionElement context)
        {
            paragraph.TextAlignment = TextAlignment.Right;
            paragraph.Margin = new Thickness(150, 0, 100, 20);

            var text = context.Text;
            if (text.StartsWith(">"))
            {
                text = text.Substring(1);
                paragraph.Inlines.Add(new Run(">") { Foreground = Brushes.Gray });
                paragraph.Inlines.Add(text.ToUpper());
            }
            else
            {
                paragraph.Foreground = Brushes.Black;
                paragraph.Inlines.Add(text.ToUpper());
            }
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
            paragraph.Margin = new Thickness(150, 0, 100, 0);

            var text = context.Text;
            if (text.StartsWith("!"))
            {
                paragraph.Inlines.Add(new Run("!") { Foreground = Brushes.Gray });
                //p.Inlines.Add(this.markdownservice.Parse(text.Substring(1)));
            }
            else
            {
                //p.Inlines.Add(this.markdownservice.Parse(text));
                paragraph.Inlines.Add(text);
            }
        }

        public override void Visit(CenteredElement context)
        {
            var text = context.Text;

            paragraph.TextAlignment = TextAlignment.Center;
            paragraph.Inlines.Add(new Run(">") { Foreground = Brushes.Gray });
            //p.Inlines.Add(this.markdownservice.Parse(text.Substring(1, text.Length - 2)));
            paragraph.Inlines.Add(text.Substring(1, text.Length -2));
            paragraph.Inlines.Add(new Run("<") { Foreground = Brushes.Gray });
        }

        public override void Visit(PageBreakElement context)
        {
            paragraph.Margin = new Thickness(150, 0, 100, 66);
            paragraph.TextAlignment = TextAlignment.Center;
            paragraph.Foreground = Brushes.Gray;
            paragraph.Inlines.Add(context.Text);   
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

