using FountainEditor;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace FountainEditorGUI
{
    class FlowVisitor : FountainEditor.FountainBaseListener
    {
        public FlowDocument DisplayDocument = new FlowDocument();
        public ObservableCollection<String> DisplayOutline = new ObservableCollection<String>();

        public override void EnterSection(FountainParser.SectionContext context)
        {
            var text = context.GetText();
            var p = new Paragraph();

            p.Margin = new Thickness(20, 0, 100, 20);
            p.Foreground = Brushes.Gray;
            p.Inlines.Add(text);

            DisplayDocument.Blocks.Add(p);
            DisplayOutline.Add(text);
        }

        public override void EnterSynopsis(FountainParser.SynopsisContext context)
        {
            var text = context.GetText();
            var p = new Paragraph();

            p.Margin = new Thickness(40, 0, 100, 20);
            p.Foreground = Brushes.Gray;
            p.Inlines.Add(text);
            
            DisplayDocument.Blocks.Add(p);
            DisplayOutline.Add(text);
        }

        public override void EnterHeading(FountainParser.HeadingContext context)
        {
            var p = new Paragraph();
            p.Margin = new Thickness(150, 0, 100, 20);
            p.FontWeight = FontWeights.Bold;

            var text = context.GetText();
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

            DisplayDocument.Blocks.Add(p);
        }

        public override void EnterTransition(FountainParser.TransitionContext context)
        {
            var p = new Paragraph();
            p.TextAlignment = TextAlignment.Right;

            var text = context.GetText();
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

            DisplayDocument.Blocks.Add(p);
        }

        public override void EnterDialog(FountainParser.DialogContext context)
        {
            var visitor = new DialogVisitor();

            DisplayDocument.Blocks.Add(visitor.VisitCharacter(context.character()));

            foreach (var item in context.dialogBlock().children)
            {
                var node = visitor.Visit(item);
                if (node != null)
                {
                    DisplayDocument.Blocks.Add(node);
                }
            }

            var p = new Paragraph();
            p.Margin = new Thickness(370, 0, 100, 0);
            p.Inlines.Add(string.Empty);

            DisplayDocument.Blocks.Add(p);
        }

        public override void EnterAction(FountainParser.ActionContext context)
        {
            var p = new Paragraph();
            p.Margin = new Thickness(150, 0, 100, 0);

            var text = context.GetText();
            if (text.StartsWith("!"))
            {
                p.Inlines.Add(new Run("!") { Foreground = Brushes.Gray });
                p.Inlines.Add(ParseMarkdown.Parse(text.Substring(1)));
            }
            else
            {
                p.Inlines.Add(ParseMarkdown.Parse(text));
            }

            DisplayDocument.Blocks.Add(p);
            DisplayDocument.Blocks.Add(new Paragraph { Margin = new Thickness(150, 0, 100, 0) });
        }

        public override void EnterCentered(FountainParser.CenteredContext context)
        {
            var text = context.GetText();

            var p = new Paragraph();
            p.TextAlignment = TextAlignment.Center;
            p.Inlines.Add(new Run(">") { Foreground = Brushes.Gray });
            p.Inlines.Add(ParseMarkdown.Parse(text.Substring(1, text.Length -2)));
            p.Inlines.Add(new Run("<") { Foreground = Brushes.Gray });
            DisplayDocument.Blocks.Add(p);
        }

        public override void EnterPageBreak(FountainParser.PageBreakContext context)
        {
            var p = new Paragraph();
            p.Margin = new Thickness(150, 0, 100, 66);
            p.TextAlignment = TextAlignment.Center;
            p.Foreground = Brushes.Gray;
            p.Inlines.Add(context.GetText());

            DisplayDocument.Blocks.Add(p);
        }

        public override void EnterTitlePage(FountainParser.TitlePageContext context)
        {
            //var text = context.GetText();
            //var p = ParseTitlePage.Parse(text);
            //p.TextAlignment = TextAlignment.Right;
            //p.Margin = new Thickness(150, 0, 100, 0);

            //displayDoc.Blocks.Add(p);
        }
    }
}
