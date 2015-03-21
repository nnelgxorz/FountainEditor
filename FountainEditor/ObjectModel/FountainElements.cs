using System.Collections.Generic;

namespace FountainEditor.ObjectModel
{
    public partial class ActionElement : Element
    {
        public ActionElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(FountainVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

    public partial class BoneyardElement : Element
    {
        public BoneyardElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(FountainVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

    public partial class CenteredElement : Element
    {
        public CenteredElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(FountainVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

    public partial class CharacterElement : Element
    {
        public CharacterElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(FountainVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

    public partial class DialogElement : Element
    {
        public DialogElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(FountainVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

    public partial class DualDialogElement : Element
    {
        public DualDialogElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(FountainVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

    public partial class LyricElement : Element
    {
        public LyricElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(FountainVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

    public partial class NoteElement : Element
    {
        public NoteElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(FountainVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

    public partial class PageBreakElement : Element
    {
        public PageBreakElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(FountainVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

    public partial class ParentheticalElement : Element
    {
        public ParentheticalElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(FountainVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

    public partial class SceneHeadingElement : Element
    {
        public SceneHeadingElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(FountainVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

    public partial class SectionElement : Element
    {
        public SectionElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(FountainVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

    public partial class SynopsisElement : Element
    {
        public SynopsisElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(FountainVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

    public partial class TransitionElement : Element
    {
        public TransitionElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(FountainVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

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

		public virtual void Visit(PageBreakElement element) { }

		public virtual void Visit(ParentheticalElement element) { }

		public virtual void Visit(SceneHeadingElement element) { }

		public virtual void Visit(SectionElement element) { }

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
