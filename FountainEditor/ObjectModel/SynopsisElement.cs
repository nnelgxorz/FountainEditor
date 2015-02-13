namespace FountainEditor.ObjectModel
{
    public sealed class SynopsisElement : Element
    {
        public SynopsisElement(string text)
            : base(text)
        {
        }

        public override void Accept(FountainVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
