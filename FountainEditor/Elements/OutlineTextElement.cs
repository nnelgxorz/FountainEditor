using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Elements
{
    public class OutlineTextElement : Element
    {
        public int Level { get; set; }

        public OutlineTextElement(string text, int level)
            : base(text)
        {
            this.Level = level;
        }
    }
}
