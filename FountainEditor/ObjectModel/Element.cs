namespace FountainEditor.ObjectModel
{
    public abstract class Element
    {
        public string Text { get; set; }

        public Element(string text)
        {
            this.Text = text;
        }

        public abstract void Accept(FountainVisitor visitor);
    }
}
