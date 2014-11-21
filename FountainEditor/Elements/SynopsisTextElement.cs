using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class SynopsisTextElement : Element
    {
        public SynopsisTextElement(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("<Synp> {0} </Synp>", Text);
        }
    }
}
