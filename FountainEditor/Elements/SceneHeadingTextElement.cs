using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class SceneHeadingTextElement : Element
    {
        public SceneHeadingTextElement(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("<ScHd> {0} </ScHd>", Text);
        }
    }
}
