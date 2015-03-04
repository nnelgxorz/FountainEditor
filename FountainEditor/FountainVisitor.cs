using System.Collections.Generic;
using FountainEditor.ObjectModel;

namespace FountainEditor
{
    public abstract class FountainVisitor
    {
        public virtual void VisitAny(Element element) { }

        public virtual void Visit(ActionElement element) { }

        public virtual void Visit(BoneyardElement element) { }

        public virtual void Visit(CenteredElement element) { }

        public virtual void Visit(CharacterElement element) { }

        public virtual void Visit(DialogElement element) { }

        public virtual void Visit(DualDialogElement element) { }

        public virtual void Visit(LyricElement element) { }

        public virtual void Visit(NoteElement element) { }

        public virtual void Visit(SectionElement element) { }

        public virtual void Visit(PageBreakElement element) { }

        public virtual void Visit(ParentheticalElement element) { }

        public virtual void Visit(SceneHeadingElement element) { }

        public virtual void Visit(SynopsisElement element) { }

        public virtual void Visit(TransitionElement element) { }

        public void VisitAll(IEnumerable<Element> elements)
        {
            foreach (var element in elements)
            {
                element.Accept(this);
            }
        }
    }
}
