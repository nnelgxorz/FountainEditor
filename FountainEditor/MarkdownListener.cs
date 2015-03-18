using FountainEditor.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor
{
    class MarkdownListener : MarkdownBaseListener
    {
        public ICollection<Element> Elements { get; private set; }

        public MarkdownListener()
        {
            Elements = new List<Element>();
        }
        public override void EnterBold(MarkdownParser.BoldContext context)
        {
            Elements.Add(new BoldElement(context.GetText()));
        }

        public override void EnterItalics(MarkdownParser.ItalicsContext context)
        {
            Elements.Add(new ItalicElement(context.GetText()));
        }

        public override void EnterBoldItalics(MarkdownParser.BoldItalicsContext context)
        {
            Elements.Add(new BoldItalicElement(context.GetText()));
        }

        public override void EnterUnderline(MarkdownParser.UnderlineContext context)
        {
            Elements.Add(new UnderlineElement(context.GetText()));
        }

        public override void EnterBoneyard(MarkdownParser.BoneyardContext context)
        {
            Elements.Add(new BoneyardElement(context.GetText()));
        }

        public override void EnterNotes(MarkdownParser.NotesContext context)
        {
            Elements.Add(new NoteElement(context.GetText()));
        }
    }
}
