namespace FountainEditor.ObjectModel
{
    public sealed class PageBreakElement : Element
    {
        public PageBreakElement(string text)
            : base(text)
        {
        }

        public override void Accept(FountainVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
