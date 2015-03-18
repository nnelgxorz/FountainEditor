using System.Collections.Generic;

namespace FountainEditor.ObjectModel
{
    public partial class BoldElement : Element
    {
        public BoldElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(MarkdownVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

    public partial class ItalicElement : Element
    {
        public ItalicElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(MarkdownVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

    public partial class BoldItalicElement : Element
    {
        public BoldItalicElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(MarkdownVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

    public partial class UnderlineElement : Element
    {
        public UnderlineElement(string text)
            : base(text)
        {
            Initialize(text);
        }

        partial void Initialize(string text);

        public override void Accept(MarkdownVisitor visitor)
        {
            visitor.VisitAny(this);
            visitor.Visit(this);
        }
    }

	public abstract class MarkdownVisitor
	{
		public virtual void VisitAny(Element element) { }

		public virtual void Visit(BoldElement element) { }

		public virtual void Visit(ItalicElement element) { }

		public virtual void Visit(BoldItalicElement element) { }

		public virtual void Visit(UnderlineElement element) { }

        public void VisitAll(IEnumerable<Element> elements)
        {
            foreach (var element in elements)
            {
                element.Accept(this);
            }
        }
	}
}
