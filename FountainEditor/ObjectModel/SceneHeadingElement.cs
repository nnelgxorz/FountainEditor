namespace FountainEditor.ObjectModel
{
    public sealed class SceneHeadingElement : Element
    {
        public SceneHeadingElement(string text)
            : base(text)
        {
        }

        public override void Accept(FountainVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
