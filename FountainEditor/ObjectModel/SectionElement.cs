namespace FountainEditor.ObjectModel
{
    public sealed class SectionElement : Element
    {
        public SectionElement(string text)
            : base(text)
        {
        }

        public override void Accept(FountainVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
