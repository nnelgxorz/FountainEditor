using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class SingleSpaceElement : Element
    {
        public SingleSpaceElement(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("<ds>{0}</ds>", Text);
        }
    }
}
