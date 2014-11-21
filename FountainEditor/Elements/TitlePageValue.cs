using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class TitlePageValue : Element
    {
        public TitlePageValue(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("<tVal> {0} </tVal>",Text);
        }
    }
}
