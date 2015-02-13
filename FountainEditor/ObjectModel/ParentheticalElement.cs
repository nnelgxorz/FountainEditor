namespace FountainEditor.ObjectModel
{
    public sealed class ParentheticalElement : Element
    {
        public ParentheticalElement(string text)
            : base(text)
        {
        }

        public override void Accept(FountainVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
