namespace FountainEditor.ObjectModel
{
    public sealed class CharacterElement : Element
    {
        public CharacterElement(string text)
            : base(text)
        {
        }

        public override void Accept(FountainVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
