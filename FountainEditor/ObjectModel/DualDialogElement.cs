namespace FountainEditor.ObjectModel
{
    public sealed class DualDialogElement : Element
    {
        public DualDialogElement(string text)
            : base(text)
        {
        }

        public override void Accept(FountainVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
