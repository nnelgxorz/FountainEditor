namespace FountainEditor.ObjectModel
{
    public sealed class TransitionElement : Element
    {
        public TransitionElement(string text)
            : base(text)
        {
        }

        public override void Accept(FountainVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
