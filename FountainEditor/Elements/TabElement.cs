using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class TabElement : Element
    {
        public TabElement(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("<t>{0}", Text);
        }
    }
}