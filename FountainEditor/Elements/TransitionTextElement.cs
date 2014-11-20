using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class TransitionTextElement : Element
    {
        public TransitionTextElement(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("<tr>{0}</tr>", Text);
        }
    }
}
