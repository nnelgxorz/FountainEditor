using System.Collections.Generic;
using System.Linq;

namespace FountainEditorGUI
{
    public sealed class TextCounter : ITextCounter
    {
        public Range<int> CountMarkdownSymbols(string text)
        {
            return new Range<int>(
                Count(text),
                Count(text.Reverse())
            );
        }

        public int CountHashTags(string text)
        {
            return text.TakeWhile(e => e.Equals('#')).Count();
        }

        private int Count(IEnumerable<char> text)
        {
            return text.TakeWhile(e => e.Equals('*') || e.Equals('_')).Count();
        }
    }
}
