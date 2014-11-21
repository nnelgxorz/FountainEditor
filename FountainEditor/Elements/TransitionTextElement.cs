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
            return string.Format("<Tran> {0} </Tran>", Text);
        }
    }
}
