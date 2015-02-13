namespace FountainEditor.ObjectModel
{
    public sealed class ActionElement : Element
    {
        public ActionElement(string text)
            : base(text)
        {
        }

        public override void Accept(FountainVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
