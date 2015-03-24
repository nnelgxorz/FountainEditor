namespace FountainEditorGUI
{
    public sealed class Range<T>
    {
        public T Start { get; private set; }
        public T End { get; private set; }

        public Range(T start, T end)
        {
            this.Start = start;
            this.End = end;
        }
    }
}
