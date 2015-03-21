namespace FountainEditor.ObjectModel
{
    public abstract class Element
    {
        public string Text { get; set; }

        public Element(string text)
        {
            this.Text = text;
        }

        public virtual void Accept(FountainVisitor visitor) { }

        public virtual void Accept(MarkdownVisitor visitor) { }
    }
}
