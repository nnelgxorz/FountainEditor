using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class LyricsTextElement : Element
    {
        public LyricsTextElement(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("<Lyrc> {0} </Lyrc>", Text);
        }
    }
}
