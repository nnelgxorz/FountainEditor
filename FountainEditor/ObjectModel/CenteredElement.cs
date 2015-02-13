namespace FountainEditor.ObjectModel
{
    public sealed class CenteredElement : Element
    {
        public CenteredElement(string text)
            : base(text)
        {
        }

        public override void Accept(FountainVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
