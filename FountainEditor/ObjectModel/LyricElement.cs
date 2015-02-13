namespace FountainEditor.ObjectModel
{
    public sealed class LyricElement : Element
    {
        public LyricElement(string text)
            : base(text)
        {
        }

        public override void Accept(FountainVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
