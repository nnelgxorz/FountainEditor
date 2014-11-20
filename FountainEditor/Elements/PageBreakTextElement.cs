using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class PageBreakTextElement : Element
    {
        public PageBreakTextElement(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("<pb>{0}</pb>", Text);
        }

    }
}
