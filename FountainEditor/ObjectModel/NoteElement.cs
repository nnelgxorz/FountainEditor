namespace FountainEditor.ObjectModel
{
    public sealed class NoteElement : Element
    {
        public NoteElement(string text)
            : base(text)
        {
        }

        public override void Accept(FountainVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
