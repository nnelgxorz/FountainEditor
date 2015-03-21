using System.Collections.Generic;
using FountainEditor.ObjectModel;

namespace FountainEditor
{
    public sealed class ElementListener : FountainBaseListener
    {
        public ICollection<Element> Elements { get; private set; }

        public ElementListener()
        {
            Elements = new List<Element>();
        }

        public override void EnterAction(FountainParser.ActionContext context)
        {
            Elements.Add(new ActionElement(context.GetText()));
        }

        public override void EnterCentered(FountainParser.CenteredContext context)
        {
            Elements.Add(new CenteredElement(context.GetText()));
        }

        public override void EnterCharacter(FountainParser.CharacterContext context)
        {
            Elements.Add(new CharacterElement(context.GetText()));
        }

        public override void EnterDialog(FountainParser.DialogContext context)
        {
            Elements.Add(new DialogElement(context.GetText()));
        }

        public override void EnterHeading(FountainParser.HeadingContext context)
        {
            Elements.Add(new SceneHeadingElement(context.GetText()));
        }

        public override void EnterLyric(FountainParser.LyricContext context)
        {
            Elements.Add(new LyricElement(context.GetText()));
        }

        public override void EnterPageBreak(FountainParser.PageBreakContext context)
        {
            Elements.Add(new PageBreakElement(context.GetText()));
        }

        public override void EnterParenthetical(FountainParser.ParentheticalContext context)
        {
            Elements.Add(new ParentheticalElement(context.GetText()));
        }
        
        public override void EnterSection(FountainParser.SectionContext context)
        {
            Elements.Add(new SectionElement(context.GetText()));
        }

        public override void EnterSynopsis(FountainParser.SynopsisContext context)
        {
            Elements.Add(new SynopsisElement(context.GetText()));
        }

        public override void EnterTransition(FountainParser.TransitionContext context)
        {
            Elements.Add(new TransitionElement(context.GetText()));
        }
    }
}
