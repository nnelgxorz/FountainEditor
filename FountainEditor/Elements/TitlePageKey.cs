using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class TitlePageKey : Element
    {
        public TitlePageKey(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("<tk>{0}</tk>", Text);
        }
    }
}
