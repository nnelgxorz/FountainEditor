using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class ParentheticalTextElement : Element
    {
        public ParentheticalTextElement(string text)
            : base(text)
        {
        }

        public override string Print()
        {
            return string.Format("<Pnth> {0} </Pnth>", Text);
        }
    }
}
