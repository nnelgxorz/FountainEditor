namespace FountainEditor.ObjectModel
{
    public sealed class BoneyardElement : Element
    {
        public BoneyardElement(string text)
            : base(text)
        {
        }

        public override void Accept(FountainVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
