using System;
using System.Collections.ObjectModel;
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

            if (text.StartsWith("."))
            {
                text = text.Substring(1);
                Run br = new Run(".");
                br.Foreground = System.Windows.Media.Brushes.LightGray;
                Run sr = new Run(text.ToUpper());
                Paragraph fp = new Paragraph();
                fp.Margin = new System.Windows.Thickness(150, 0, 100, 20);
                fp.FontWeight = System.Windows.FontWeights.Bold;
                fp.Inlines.Add(br);
                fp.Inlines.Add(sr);
                displayDoc.Blocks.Add(fp);
            }

            else
            {
                Run r = new Run(text.ToUpper());
                Paragraph p = new Paragraph();

                p.Margin = new System.Windows.Thickness(150, 0, 100, 20);
                p.FontWeight = System.Windows.FontWeights.Bold;

                p.Inlines.Add(r);
                displayDoc.Blocks.Add(p);
            }
        }

        public override void EnterTransition(FountainEditor.FountainParser.TransitionContext context)
        {
            string text = context.GetText();
            if (text.StartsWith(">"))
            {
                text = text.Substring(1);
                Run fr = new Run(">");
                fr.Foreground = System.Windows.Media.Brushes.LightGray;
                Run sr = new Run(text.ToUpper());
                Paragraph fp = new Paragraph();
                fp.TextAlignment = System.Windows.TextAlignment.Right;
                fp.Inlines.Add(fr);
                fp.Inlines.Add(sr);
                displayDoc.Blocks.Add(fp);
            }
            else
            {
                Run r = new Run(text.ToUpper());
                Paragraph p = new Paragraph();

                p.Foreground = System.Windows.Media.Brushes.Black;
                p.TextAlignment = System.Windows.TextAlignment.Right;
                p.Inlines.Add(r);
                displayDoc.Blocks.Add(p);
            }
        }

        public override void EnterDialog(FountainEditor.FountainParser.DialogContext context)
        {
            var visitor = new DialogVisitor();

            displayDoc.Blocks.Add(visitor.VisitCharacter(context.character()));

            foreach (var item in context.dialogBlock().children)
            {
                var node = visitor.Visit(item);
                if (node != null)
                {
                    displayDoc.Blocks.Add(node);
                }
            }

            Paragraph p = new Paragraph();
            Run r = new Run("");
            p.Margin = new System.Windows.Thickness(370, 0, 100, 0);
            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
        }

        public override void EnterAction(FountainEditor.FountainParser.ActionContext context)
        {
            string text = context.GetText();
            Run r = new Run(text);
            Run e = new Run("");
            Paragraph ep = new Paragraph();
            Paragraph p = new Paragraph();
            p.Margin = new System.Windows.Thickness(150, 0, 100, 0);
            ep.Margin = new System.Windows.Thickness(150, 0, 100, 0);


            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
            displayDoc.Blocks.Add(ep);
        }

        public override void EnterCentered(FountainEditor.FountainParser.CenteredContext context)
        {
            string text = context.GetText();
            text = text.Substring(1);
            text = text.Substring(0, text.Length - 1);
            Run fR = new Run(">");
            fR.Foreground = System.Windows.Media.Brushes.LightGray;
            Run r = new Run(text);
            Run eR = new Run("<");
            eR.Foreground = System.Windows.Media.Brushes.LightGray;
            Paragraph p = new Paragraph();

            p.TextAlignment = System.Windows.TextAlignment.Center;
            p.Inlines.Add(fR);
            p.Inlines.Add(r);
            p.Inlines.Add(eR);
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

            p.Foreground = System.Windows.Media.Brushes.Red;
            p.TextAlignment = System.Windows.TextAlignment.Right;

            p.Margin = new System.Windows.Thickness(150, 0, 100, 0);
            p.Inlines.Add(r);
            displayDoc.Blocks.Add(p);
        }
    }
}
