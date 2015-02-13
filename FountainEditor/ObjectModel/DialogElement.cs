namespace FountainEditor.ObjectModel
{
    public sealed class DialogElement : Element
    {
        public DialogElement(string text)
            : base(text)
        {
        }

        public override void Accept(FountainVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
