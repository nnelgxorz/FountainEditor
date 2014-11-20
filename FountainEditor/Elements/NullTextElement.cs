using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class NullTextElement : Element
    {
        public NullTextElement(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("<NU>{0}</NU>", Text);
        }
    }
}
