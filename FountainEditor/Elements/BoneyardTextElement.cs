using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class BoneyardTextElement : Element
    {
        public BoneyardTextElement(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("<A>{0}</A>", Text);
        }
    }
}
